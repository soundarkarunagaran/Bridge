using Bridge.Test.NUnit;
using System;
using System.Reflection;
using System.Threading;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3821 - {0}")]
    public class Bridge3821
    {
        [Test(ExpectedCount = 1)]
        public async static void TestWebSocketError()
        {
            var done = Assert.Async();
            try
            {               
                var sc = new System.Net.WebSockets.ClientWebSocket();
                await sc.ConnectAsync(new Uri("wss://NotExistServer/NotExist"), System.Threading.CancellationToken.None);
                Assert.Fail();
                done();
            }
            catch (Exception ex)
            {
                Assert.NotNull(ex);
                done();
            }
        }
    }
}