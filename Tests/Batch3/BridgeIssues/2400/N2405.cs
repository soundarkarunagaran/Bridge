using System;
using System.Threading;
using System.Threading.Tasks;
using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#2405 - {0}")]
    public class Bridge2405
    {
        /*[Test]
        public static void TestDelayCancel()
        {
            var done = Assert.Async();

            CancellationTokenSource source = new CancellationTokenSource();

            var t = Task.Run(async delegate
            {
                await Task.Delay(100, source.Token);

                return 42;
            });

            source.Cancel();

            try
            {
                t.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    Assert.True(e is TaskCanceledException);
                }
            }

            Assert.AreEqual(TaskStatus.Canceled, t.Status);

            source.Dispose();
            done();
        }*/
    }
}