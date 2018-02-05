using Bridge.Html5;
using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3404 - {0}")]
    public class Bridge3404
    {
        [Test]
        public static void TestExtensionMethodDecimal()
        {
            decimal a = 0;
            Assert.AreEqual("text", a.M("text"));
        }
    }

    public static class Bridge3404Ex
    {
        public static string M(this decimal a, string b)
        {
            return b;
        }
    }
}