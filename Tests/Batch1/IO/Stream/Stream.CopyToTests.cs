// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bridge.ClientTest.IO
{
    [Category(Constants.MODULE_IO)]
    [TestFixture(TestNameFormat = "StreamCopyToTests - {0}")]
    public class StreamCopyToTests
    {
        [Test]
        public void IfCanSeekIsFalseLengthAndPositionShouldNotBeCalled()
        {
            var baseStream = new DelegateStream(
                canReadFunc: () => true,
                canSeekFunc: () => false,
                readFunc: (buffer, offset, count) => 0);
            var trackingStream = new CallTrackingStream(baseStream);

            var dest = Stream.Null;
            trackingStream.CopyTo(dest);

            var times = trackingStream.TimesCalled(nameof(trackingStream.CanSeek));
            Assert.True(times >= 0 && times <= 1);
            Assert.AreEqual(0, trackingStream.TimesCalled(nameof(trackingStream.Length)));
            Assert.AreEqual(0, trackingStream.TimesCalled(nameof(trackingStream.Position)));
            // We can't override CopyTo since it's not virtual, so checking TimesCalled
            // for CopyTo will result in 0. Instead, we check that Read was called,
            // and validate the parameters passed there.
            Assert.AreEqual(1, trackingStream.TimesCalled(nameof(trackingStream.Read)));

            byte[] outerBuffer = trackingStream.ReadBuffer;
            int outerOffset = trackingStream.ReadOffset;
            int outerCount = trackingStream.ReadCount;

            Assert.NotNull(outerBuffer);
            Assert.True(outerOffset >= 0 && outerOffset <= (outerBuffer.Length - outerCount));
            Assert.True(outerCount >= 1 && outerCount <= int.MaxValue); // the buffer can't be size 0
        }

        [Test]
        public void AsyncIfCanSeekIsFalseLengthAndPositionShouldNotBeCalled()
        {
            var baseStream = new DelegateStream(
                canReadFunc: () => true,
                canSeekFunc: () => false,
                readFunc: (buffer, offset, count) => 0);
            var trackingStream = new CallTrackingStream(baseStream);

            var dest = Stream.Null;
            trackingStream.CopyTo(dest);

            var times = trackingStream.TimesCalled(nameof(trackingStream.CanSeek));
            Assert.True(times >= 0 && times <= 1);
            Assert.AreEqual(0, trackingStream.TimesCalled(nameof(trackingStream.Length)));
            Assert.AreEqual(0, trackingStream.TimesCalled(nameof(trackingStream.Position)));
        }

        [Test]
        public void IfCanSeekIsTrueLengthAndPositionShouldOnlyBeCalledOnce()
        {
            var baseStream = new DelegateStream(
                canReadFunc: () => true,
                canSeekFunc: () => true,
                readFunc: (buffer, offset, count) => 0,
                lengthFunc: () => 0L,
                positionGetFunc: () => 0L);
            var trackingStream = new CallTrackingStream(baseStream);

            var dest = Stream.Null;
            trackingStream.CopyTo(dest);

            var times = trackingStream.TimesCalled(nameof(trackingStream.Length));
            Assert.True(times >= 0 && times <= 1);

            times = trackingStream.TimesCalled(nameof(trackingStream.Position));
            Assert.True(times >= 0 && times <= 1);
        }

        [Test]
        public void AsyncIfCanSeekIsTrueLengthAndPositionShouldOnlyBeCalledOnce()
        {
            var baseStream = new DelegateStream(
                canReadFunc: () => true,
                canSeekFunc: () => true,
                readFunc: (buffer, offset, count) => 0,
                lengthFunc: () => 0L,
                positionGetFunc: () => 0L);
            var trackingStream = new CallTrackingStream(baseStream);

            var dest = Stream.Null;
            trackingStream.CopyTo(dest);

            var times = trackingStream.TimesCalled(nameof(trackingStream.Length));
            Assert.True(times >= 0 && times <= 1);

            times = trackingStream.TimesCalled(nameof(trackingStream.Position));
            Assert.True(times >= 0 && times <= 1);
        }

        [Test]
        public void IfLengthIsLessThanOrEqualToPositionCopyToShouldStillBeCalledWithAPositiveBufferSize()
        {
            var data = LengthIsLessThanOrEqualToPosition();
            foreach (var item in data)
            {
                long length = (long)item[0];
                long position = (long)item[1];

                // Streams with their Lengths <= their Positions, e.g.
                // new MemoryStream { Position = 3 }.SetLength(1)
                // should still be called CopyTo{Async} on with a
                // bufferSize of at least 1.

                var baseStream = new DelegateStream(
                    canReadFunc: () => true,
                    canSeekFunc: () => true,
                    lengthFunc: () => length,
                    positionGetFunc: () => position,
                    readFunc: (buffer, offset, count) => 0);
                var trackingStream = new CallTrackingStream(baseStream);

                var dest = Stream.Null;
                trackingStream.CopyTo(dest);

                // CopyTo is not virtual, so we can't override it in
                // CallTrackingStream and record the arguments directly.
                // Instead, validate the arguments passed to Read.

                Assert.AreEqual(1, trackingStream.TimesCalled(nameof(trackingStream.Read)));

                byte[] outerBuffer = trackingStream.ReadBuffer;
                int outerOffset = trackingStream.ReadOffset;
                int outerCount = trackingStream.ReadCount;

                Assert.NotNull(outerBuffer);
                Assert.True(outerOffset >= 0 && outerOffset <= (outerBuffer.Length - outerCount));
                Assert.True(outerCount >= 1 && outerCount <= int.MaxValue);
            }
        }

        [Test]
        public void IfLengthMinusPositionPositiveOverflowsBufferSizeShouldStillBePositive()
        {
            var data = LengthMinusPositionPositiveOverflows();
            foreach (var item in data)
            {
                long length = (long)item[0];
                long position = (long)item[1];
                // The new implementation of Stream.CopyTo calculates the bytes left
                // in the Stream by calling Length - Position. This can overflow to a
                // negative number, so this tests that if that happens we don't send
                // in a negative bufferSize.

                var baseStream = new DelegateStream(
                canReadFunc: () => true,
                canSeekFunc: () => true,
                lengthFunc: () => length,
                positionGetFunc: () => position,
                readFunc: (buffer, offset, count) => 0);
                var trackingStream = new CallTrackingStream(baseStream);

                var dest = Stream.Null;
                trackingStream.CopyTo(dest);

                // CopyTo is not virtual, so we can't override it in
                // CallTrackingStream and record the arguments directly.
                // Instead, validate the arguments passed to Read.

                Assert.AreEqual(1, trackingStream.TimesCalled(nameof(trackingStream.Read)));

                byte[] outerBuffer = trackingStream.ReadBuffer;
                int outerOffset = trackingStream.ReadOffset;
                int outerCount = trackingStream.ReadCount;

                Assert.NotNull(outerBuffer);
                Assert.True(outerOffset >= 0 && outerOffset <= (outerBuffer.Length - outerCount));
                Assert.True(outerCount >= 1 && outerCount <= int.MaxValue);
            }
        }

        [Test]
        public void IfLengthIsGreaterThanPositionAndDoesNotOverflowEverythingShouldGoNormally()
        {
            const int ReadLimit = 7;

            var data = LengthIsGreaterThanPositionAndDoesNotOverflow();
            foreach (var item in data)
            {
                long length = (long)item[0];
                long position = (long)item[1];

                // Lambda state
                byte[] outerBuffer = null;
                int? outerOffset = null;
                int? outerCount = null;
                int readsLeft = ReadLimit;

                var srcBase = new DelegateStream(
                    canReadFunc: () => true,
                    canSeekFunc: () => true,
                    lengthFunc: () => length,
                    positionGetFunc: () => position,
                    readFunc: (buffer, offset, count) =>
                    {
                        Assert.NotNull(buffer);
                        Assert.True(offset >= 0 && offset <= (buffer.Length - count));
                        Assert.True(count >= 1 && count <= int.MaxValue);

                        // CopyTo should always pass in the same buffer/offset/count

                        if (outerBuffer != null) Assert.AreDeepEqual(outerBuffer, buffer);
                        else outerBuffer = buffer;

                        if (outerOffset != null) Assert.AreEqual(outerOffset, offset);
                        else outerOffset = offset;

                        if (outerCount != null) Assert.AreEqual(outerCount, count);
                        else outerCount = count;

                        return --readsLeft; // CopyTo will call Read on this ReadLimit times before stopping
                    });

                var src = new CallTrackingStream(srcBase);

                var destBase = new DelegateStream(
                    canWriteFunc: () => true,
                    writeFunc: (buffer, offset, count) =>
                    {
                        Assert.AreDeepEqual(outerBuffer, buffer);
                        Assert.AreEqual(outerOffset, offset);
                        Assert.AreEqual(readsLeft, count);
                    });

                var dest = new CallTrackingStream(destBase);
                src.CopyTo(dest);

                Assert.AreEqual(ReadLimit, src.TimesCalled(nameof(src.Read)));
                Assert.AreEqual(ReadLimit - 1, dest.TimesCalled(nameof(dest.Write)));
            }
        }

        // Member data

        public static IEnumerable<object[]> LengthIsLessThanOrEqualToPosition()
        {
            yield return new object[] { 5L, 5L }; // same number
            yield return new object[] { 3L, 5L }; // length is less than position
            yield return new object[] { -1L, -1L }; // negative numbers
            yield return new object[] { 0L, 0L }; // both zero
            yield return new object[] { -500L, 0L }; // negative number and zero
            yield return new object[] { 0L, 500L }; // zero and positive number
            yield return new object[] { -500L, 500L }; // negative and positive number
            yield return new object[] { long.MinValue, long.MaxValue }; // length - position <= 0 will fail (overflow), but length <= position won't
        }

        public static IEnumerable<object[]> LengthMinusPositionPositiveOverflows()
        {
            yield return new object[] { long.MaxValue, long.MinValue }; // length - position will be -1
            yield return new object[] { 1L, -long.MaxValue };
        }

        public static IEnumerable<object[]> LengthIsGreaterThanPositionAndDoesNotOverflow()
        {
            yield return new object[] { 5L, 3L };
            yield return new object[] { -3L, -6L };
            yield return new object[] { 0L, -3L };
            yield return new object[] { long.MaxValue, 0L }; // should not overflow or OOM
            yield return new object[] { 85000L, 123L }; // at least in the current implementation, we max out the bufferSize at 81920
        }
    }
}