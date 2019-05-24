using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3969 - {0}")]
    public class Bridge3969
    {
        [Test]
        public static void TestCharIndexer()
        {
            string[] testarr = new string[255];
            testarr['*'] = "works";
            testarr[(int)'h'] = "also works";
            testarr['h'] = "wait what?";

            Assert.AreEqual("works", testarr['*']);
            Assert.AreEqual("works", testarr[(int)'*']);

            Assert.AreEqual("wait what?", testarr[(int)'h']);
            Assert.AreEqual("wait what?", testarr['h']);
        }
    }
}