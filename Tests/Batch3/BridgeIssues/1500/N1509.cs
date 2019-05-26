using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#1509 - {0}")]
    public class Bridge1509
    {
        [Test]
        public void TestPreformanceNowIsDouble()
        {
            double p;
            for (int i = 1; i < 1000; i++)
            {
                p = Bridge.Html5.Global.Performance.Now();
                if (!HasNoFraction(p))
                {
                    Assert.True(true, "Returned float from performance.now() in " + i + "/1000 attempts.");
                    return;
                }
            }

            /*
             * Firefox 60+ would never return a float from performance.now.
             * According to
             * https://developer.mozilla.org/en-US/docs/Web/API/Performance/now:
             *   "The timestamp is not actually high-resolution. To mitigate
             *    security threats such as Spectre, browsers currently round
             *    the results to varying degrees.
             * >>>(Firefox started rounding to 1 millisecond in Firefox 60.)
             *    Some browsers may also slightly randomize the timestamp. The
             *    precision may improve again in future releases; browser
             *    developers are still investigating these timing attacks and
             *    how best to mitigate them."
             */
            if (Browser.FirefoxVersion >= 60)
            {
                Assert.True(true, "Firefox 60+ has performance.now() return exact 1ms-step values.");
                return;
            }
            else
            {
                Assert.Fail("Not returned float from performance.now() in 1000 attemps.");
            }
        }

        private bool HasNoFraction(double n)
        {
            return n % 1 == 0;
        }
    }
}
 
 