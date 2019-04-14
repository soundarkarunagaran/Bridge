using Bridge.Test.NUnit;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3898 - {0}")]
    public class Bridge3898
    {
        [Test]
        public static void TestNegativeZero()
        {
            double negativezero = -0.0;
            Assert.True(double.IsNegativeInfinity(1 / negativezero));
        }
    }
}