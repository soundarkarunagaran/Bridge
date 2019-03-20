using Bridge.Test.NUnit;
using System.Reflection;
using System.Threading.Tasks;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3871 - {0}")]
    public class Bridge3871
    {
        static Task<bool> something()
        {
            return Task.FromResult(true);
        }

        [Test]
        public static void TestAsyncLocalFunction()
        {
            var done = Assert.Async();
            async void fct()
            {
                var r = await something();
                Assert.True(r);
                done();
            }
            fct();
        }
    }
}