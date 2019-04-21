// this is required in order to reproduce the issue (read below)
using Bridge.Html5;
using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3902 - {0}")]
    public class Bridge3902

    {
        [Test]
        public static void TestUnicodeEscapes()
        {
            string s = "\ud8a1\ud8a2\ue681";
            Assert.AreEqual(55457, (int)s[0]);
            Assert.AreEqual(55458, (int)s[1]);
            Assert.AreEqual(59009, (int)s[2]);
        }
    }
}