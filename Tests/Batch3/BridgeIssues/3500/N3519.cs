using Bridge.Html5;
using Bridge.Test.NUnit;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3519 - {0}")]
    public class Bridge3519
    {
        private static readonly string[] Keys = { "a", "b" };

        private static readonly Dictionary<string, string> SMap = new Dictionary<string, string>();

        private static Dictionary<string, string> vMap = new Dictionary<string, string>();

        private static string RegExpEscape(string s)
        {
            // because of this string magic happens :)
            return Bridge.Script.Write<string>("{0}.replace(/[-\\/\\^$*+?.()|[\\]{}]/g, '\\\\$&');", s);
        }

        [Test]
        public static void TestInjectScript()
        {
            SMap["a"] = "b";
            SMap["c"] = "d";

            foreach (string vote in Keys)
            {
                // colon and \n missing
                vMap[vote] = vote;
                int a = 1;
                Assert.AreEqual(1, a);
            }

            Assert.AreEqual("b", SMap["a"]);
            Assert.AreEqual("d", SMap["c"]);
        }
    }
}