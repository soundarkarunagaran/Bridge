using Bridge.Test.NUnit;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3905 - {0}")]
    public class Bridge3905
    {
        [Test]
        public static void TestArrayIncrement()
        {
            int[] a = new int[] { 3, 8 };
            int i = 0;
            a[i++]++;
            Assert.AreEqual(1, i);
            Assert.AreEqual(4, a[0]);
            Assert.AreEqual(8, a[1]);
        }
    }
}