using System;
using Bridge.Html5;
using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#3321 - {0}")]
    public class Bridge3321
    {
        [Test]
        public static void Test2DArrayClone()
        {
            var n = (int[,])new int[0, 0].Clone();

            Assert.AreEqual(2, n.Rank);
            Assert.AreEqual(typeof(int), n.GetType().GetElementType());
        }
    }
}