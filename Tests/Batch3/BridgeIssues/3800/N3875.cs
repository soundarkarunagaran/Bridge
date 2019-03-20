using Bridge.Test.NUnit;
using System.Linq;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3875 - {0}")]
    public class Bridge3875
    {
        [Test]
        public static void TestToArray()
        {
            var bytes = new byte[] { 1, 2, 3, 4 };
            Assert.AreEqual("Byte[]", bytes.ToArray().GetType().Name);
            Assert.AreEqual("Byte[]", bytes.GetType().Name);
        }
    }
}