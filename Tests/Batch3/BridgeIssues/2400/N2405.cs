using System;
using System.Threading;
using System.Threading.Tasks;
using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#2405 - {0}")]
    public class Bridge2405
    {
        [Test]
        public static void TestDelayWithCancelation()
        {
            var done = Assert.Async();
            CancellationTokenSource source = new CancellationTokenSource();

            var t = Task.Run(async delegate
            {
                // Support required for .Delay with Token
                await Task.Delay(100, source.Token);

                return 42;
            });

            source.Cancel();

            try
            {
                // Support required for .Wait
                t.Wait();
            }
            catch (AggregateException ae)
            {
                Assert.True(ae.InnerException is TaskCanceledException);
            }

            Assert.AreEqual(TaskStatus.Canceled, t.Status);

            source.Dispose();
            done();
        }

        [Test]
        public static void TestDelayWithCancelation2()
        {
            var done = Assert.Async();
            CancellationTokenSource source = new CancellationTokenSource();

            var t = Task.Run(async delegate
            {
                // Support required for .Delay with Token
                await Task.Delay(100, source.Token);

                return 42;
            });            

            try
            {
                t.Wait(200);
                source.Cancel();
            }
            catch (AggregateException)
            {
                Assert.Fail("Should not be an exception");
            }

            Assert.AreEqual(TaskStatus.RanToCompletion, t.Status);
            Assert.AreEqual(42, t.Result);

            source.Dispose();
            done();
        }
    }
}