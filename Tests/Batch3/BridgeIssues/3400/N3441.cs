using Bridge.Test.NUnit;
using System;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    /// <summary>
    /// The test here consists in checking whether a two-level interface
    /// inheritance cast works as expected when a member is overridden thru
    /// the inheritance path.
    /// </summary>
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#3441 - {0}")]
    public class Bridge3441
    {
        [Test]
        public static void TestNegativeTimeSpanValueToString()
        {
            var val1 = (DateTime.Today - DateTime.Today.AddSeconds(7)).ToString();
            var val2 = (DateTime.Today - DateTime.Today.AddSeconds(70)).ToString();
            var val3 = (DateTime.Today - DateTime.Today.AddMinutes(7)).ToString();
            var val4 = (DateTime.Today - DateTime.Today.AddMinutes(70)).ToString();
            var val5 = (DateTime.Today - DateTime.Today.AddHours(7)).ToString();
            var val6 = (DateTime.Today - DateTime.Today.AddHours(70)).ToString();
            var val7 = (DateTime.Today - DateTime.Today.AddHours(700)).ToString();
            var val8 = (DateTime.Today - DateTime.Today.AddHours(7000)).ToString();

            Assert.AreEqual("-00:00:07", val1);
            Assert.AreEqual("-00:01:10", val2);
            Assert.AreEqual("-00:07:00", val3);
            Assert.AreEqual("-01:10:00", val4);
            Assert.AreEqual("-07:00:00", val5);
            Assert.AreEqual("-2.22:00:00", val6);
            Assert.AreEqual("-29.04:00:00", val7);
            Assert.AreEqual("-291.16:00:00", val8);
        }
    }
}