using Bridge.Test.NUnit;
using System;
using System.Threading.Tasks;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3951 - {0}")]
    public class Bridge3951
    {
        [Test]
        public static void TestAwaitFinished()
        {
            int[] count = new int[1];
            Task t = TightLoop(count);
            Assert.AreEqual(10000, count[0]);
        }

        public static async Task TightLoop(int[] count)
        {
            Task finished = Task.FromResult<String>(null);
            for (int i = 0; i < 10000; i++)
            {
                await finished;
                count[0]++;
            }
        }
    }
}