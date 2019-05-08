using Bridge.Test.NUnit;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3883 - {0}")]
    public class Bridge3883
    {
        [Test]
        public static void TestPlain64()
        {
            ulong ul = Script.Write<ulong>("9223372036854775808");
            long l = (long)ul;

            Assert.True(l == long.MinValue);
        }
    }
}