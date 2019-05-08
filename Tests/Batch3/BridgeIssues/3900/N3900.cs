using Bridge.Test.NUnit;
using System;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3900 - {0}")]
    public class Bridge3900
    {
        [Test]
        public static void TestInfinitySign()
        {
            Assert.AreEqual(1, Math.Sign(Double.PositiveInfinity));
            Assert.AreEqual(-1, Math.Sign(Double.NegativeInfinity));
        }
    }
}