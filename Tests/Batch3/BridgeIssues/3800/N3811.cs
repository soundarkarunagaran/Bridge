using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3811 - {0}")]
    public class Bridge3811
    {
        public class Settings
        {
            public const long DefaultMinValue1 = long.MinValue;
            public const long DefaultMinValue2 = -9223372036854775808L;
            public const long DefaultMaxValue1 = long.MaxValue;
            public const long DefaultMaxValue2 = 9223372036854775807L;
        }

        [Test]
        public static void TestLongMinValue()
        {
            var min = -9223372036854775808L;
            var max = 9223372036854775807L;
            Assert.AreEqual(min, Settings.DefaultMinValue1);
            Assert.AreEqual(min, Settings.DefaultMinValue2);

            Assert.AreEqual(max, Settings.DefaultMaxValue1);
            Assert.AreEqual(max, Settings.DefaultMaxValue2);
        }
    }
}