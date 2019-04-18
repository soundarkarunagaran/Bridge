using Bridge.Test.NUnit;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3887 - {0}")]
    public class Bridge3887
    {
        [Test]
        public static void TestDecimalToString()
        {
            Assert.AreEqual("0", 0m.ToString());
            Assert.AreEqual("0.0", 0.0m.ToString());
            Assert.AreEqual("0.00", 0.00m.ToString());

            var d1 = 0.314m;
            var d2 = 0.0140m;
            Assert.AreEqual("0.3000", (d1 - d2).ToString());

            d1 = 0.314m;
            d2 = 0.014m;
            Assert.AreEqual("0.300", (d1 - d2).ToString());

            d1 = 0.300m;
            d2 = 0.01m;
            Assert.AreEqual("0.310", (d1 + d2).ToString());

            d1 = 0.06m;
            d2 = d1 / 4;
            Assert.AreEqual("0.060", (d2 * 4).ToString());
        }
    }
}