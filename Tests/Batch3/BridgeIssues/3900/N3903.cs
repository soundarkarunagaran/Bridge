using Bridge.Test.NUnit;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3903 - {0}")]
    public class Bridge3903
    {
        [Test]
        public static void TestBooleanXor()
        {
            var t = true;
            var f = false;
            var a = t ^ f;
            var b = t;
            b ^= t;

            Assert.True(a == true);
            Assert.True(b == false);
        }
    }
}