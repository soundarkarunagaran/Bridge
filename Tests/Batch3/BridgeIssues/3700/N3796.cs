using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3796 - {0}")]
    public class Bridge3796
    {
        [Test]
        public static void TestInt64Cast()
        {
            double double1 = 0L;
            var result = double1 / double1;
            Assert.True((long)(result) == long.MinValue);
            Assert.True(Double.IsNaN(result));
        }
    }
}