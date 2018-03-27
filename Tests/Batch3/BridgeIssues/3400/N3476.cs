using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#3476 - {0}")]
    public class Bridge3476
    {
        public class Temp
        {
            public Temp(Action done)
            {
                testData = true;
                this.done = done;
                source = new TaskCompletionSource<object>();
            }

            public void SomeEvent1()
            {
                Method1().ContinueWith(
                    r_ => {
                    // something
                });
            }

            public void SomeEvent2()
            {
                Method2().ContinueWith(
                    r_ => {
                    // something
                });
            }

            public async Task<string> Method1()
            {
                // wait for second click
                // BTW: remove this line and compiler will fail
                object oResult = await source.Task;

                // do some finishing
                return await Method4();
            }

            public async Task<string> Method2()
            {
                //wake up second activity
                source.SetResult(null);

                //do some finishing
                return await Method3();
            }

            public async Task<string> Method3()
            {
                // call whatever
                await Method5();

                testData = false;

                return "";
            }

            public async Task<string> Method4()
            {
                // Expected: True
                Assert.True(testData);

                await Method5();

                // Expected: True
                Assert.True(testData);
                done();
                return "";
            }

            public Task<string> Method5()
            {
                return Task.FromResult<string>("");
            }

            bool testData;
            private Action done;
            TaskCompletionSource<object> source;
        }

        [Test(ExpectedCount = 2)]
        public async static void TestTaskCompletionSource()
        {
            var done = Assert.Async();
            var temp = new Temp(done);

            temp.SomeEvent1();

            await Task.Delay(1000);

            temp.SomeEvent2();
        }
    }
}