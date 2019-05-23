using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3958 - {0}")]
    public class Bridge3964
    {
        [Test]
        public static void TestIndexOfAny()
        {
            char[] test = { 'd', 'H' };
            var msg = "Hello, World!";

            Assert.AreEqual(0, msg.IndexOfAny(test));

            char[] chars = { 'a', 'e', 'i', 'o', 'u', 'y',
                       'A', 'E', 'I', 'O', 'U', 'Y' };
            var s = "The long and winding road...";
            Assert.AreEqual(2, s.IndexOfAny(chars));

            var str = "Now is the time for all good men to come to the aid of their party.";
            string target = "is";
            char[] anyOf = target.ToCharArray();
            int  start = str.Length / 2;
            Assert.AreEqual(49, str.IndexOfAny(anyOf, start));

            target = "aid";
            anyOf = target.ToCharArray();
            start = (str.Length - 1) / 3;
            var count = (str.Length - 1) / 4;
            Assert.AreEqual(27, str.IndexOfAny(anyOf, start, count));
        }
    }
}