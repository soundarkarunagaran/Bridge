using Bridge.Test.NUnit;
using System;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3901 - {0}")]
    public class Bridge3901
    {
        [Test]
        public static void TestToHexString()
        {
            sbyte b = -1;
            Assert.AreEqual("FF", b.ToString("X"));

            Int16 i16 = -1;
            Assert.AreEqual("FFFF", i16.ToString("X"));

            Int32 i32 = -1;
            Assert.AreEqual("FFFFFFFF", i32.ToString("X"));

            Int64 i64 = -1;
            Assert.AreEqual("FFFFFFFFFFFFFFFF", i64.ToString("X"));

            int i = -423433;
            Assert.AreEqual("fff989f7", i.ToString("x"));
            Assert.AreEqual("FFF989F7", i.ToString("X"));
        }
    }
}