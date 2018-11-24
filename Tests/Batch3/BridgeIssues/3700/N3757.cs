using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3757 - {0}")]
    public class Bridge3757
    {
        [Test]
        public static void TestDate()
        {
            var a = new System.DateTime(2001, 2, 3, 4, 5, 6).Date;
            var b = new System.DateTime(2001, 2, 3, 4, 5, 7);
            var c = new System.DateTime(2001, 2, 3, 4, 5, 8).Date;

            Assert.True(!b.Equals(a)); //gist of issue: throws exception due to wrong DateTime.js $clearTime
            //next two are safety guards that fix doesn't introduce new bugs
            Assert.True(!a.Equals(b));
            Assert.True(a.Equals(c));
        }
    }
}
