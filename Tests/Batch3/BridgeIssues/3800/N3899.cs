using Bridge.Test.NUnit;
using System;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3899 - {0}")]
    public class Bridge3899
    {
        [Test]
        public static void TestIEEERemainder()
        {
            Assert.AreEqual(-1, Math.Round(Math.IEEERemainder(3, 2), 1));
            Assert.AreEqual(0, Math.Round(Math.IEEERemainder(4, 2), 1));
            Assert.AreEqual(1, Math.Round(Math.IEEERemainder(10, 3), 1));
            Assert.AreEqual(-1, Math.Round(Math.IEEERemainder(11, 3), 1));
            Assert.AreEqual(-1, Math.Round(Math.IEEERemainder(27, 4), 1));
            Assert.AreEqual(-2, Math.Round(Math.IEEERemainder(28, 5), 1));
            Assert.AreEqual(1.8, Math.Round(Math.IEEERemainder(17.8, 4), 1));
            Assert.AreEqual(1.4, Math.Round(Math.IEEERemainder(17.8, 4.1), 1));
            Assert.AreEqual(0.1, Math.Round(Math.IEEERemainder(-16.3, 4.1), 1));
            Assert.AreEqual(1.4, Math.Round(Math.IEEERemainder(17.8, -4.1), 1));
            Assert.AreEqual(-1.4, Math.Round(Math.IEEERemainder(-17.8, -4.1), 1));
            Assert.AreEqual(1, Math.Round(Math.IEEERemainder(-3, 2), 1));
            Assert.AreEqual(1, Math.Round(Math.IEEERemainder(5, 2), 1));
            Assert.AreEqual(2, Math.Round(Math.IEEERemainder(2, Double.NegativeInfinity), 1));
            Assert.AreEqual(-4.3, Math.Round(Math.IEEERemainder(-4.3, Double.NegativeInfinity), 1));
        }
    }
}