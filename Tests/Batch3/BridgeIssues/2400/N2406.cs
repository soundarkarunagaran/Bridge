using System;
using System.Threading.Tasks;
using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#2406 - {0}")]
    public class Bridge2406
    {
        [Test]
        public static void TestTaskWait()
        {
            var done = Assert.Async();

            string buffer = "";
            var result = Task.Run(() =>
            {
                buffer += "1";
            });

            result.ContinueWith(
                (test) =>
                {
                    buffer += "2";
                }).Wait();

            buffer += "3";

            Assert.AreEqual("123", buffer);
            done();
        }

        [Test]
        public async static void TestTaskWait2()
        {
            var done = Assert.Async();
            string buffer = "";

            var result = Task.Run(() =>
            {
                buffer += "1";
            });

            result.ContinueWith(
                (test) =>
                {
                    buffer += "2";
                }).Wait();

            buffer += "3";

            buffer += "4";
            await Task.Delay(100);
            buffer += "5";

            result = Task.Run(() =>
            {
                buffer += "6";
            });

            buffer += "7";
            await Task.Delay(100);
            buffer += "8";

            result.ContinueWith(
                (test) =>
                {
                    buffer += "9";
                }).Wait();

            buffer += "10";
            await Task.Delay(100);
            buffer += "11";

            buffer += "12";

            Assert.AreEqual("123457689101112", buffer);

            done();
        }
    }
}