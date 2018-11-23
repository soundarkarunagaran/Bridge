using Bridge.Test.NUnit;
using System.Threading.Tasks;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3743 - {0}")]
    public class Bridge3743
    {
        async static System.Threading.Tasks.Task<int> TestAsync()
        {
            int i = 0, r = 0, c = 0;
            for (int a = r - i, b = c - i; b <= c + i; b++)
            {
            }

            await Task.Delay(1);
            return 1;
        }

        [Test]
        public async static void TestForLoopAsync()
        {
            var done = Assert.Async();

            var r = await TestAsync();
            Assert.AreEqual(1, r);

            done();
        }
    }
}