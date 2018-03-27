using Bridge.Test.NUnit;

namespace Bridge.ClientTest.CSharp7
{
    /// <summary>
    /// The test here consists in checking C# 7's 'is' syntax support.
    /// </summary>
    [Category(Constants.MODULE_BASIC_CSHARP)]
    [TestFixture(TestNameFormat = "Local function - {0}")]
    public class TestLocalFunctions
    {
        /// <summary>
        /// Explores several 'is' pattern syntax variations, and fail if they
        /// do not behave the expected way.
        /// </summary>
        [Test(ExpectedCount = 3)]
        public static void LocalFunctionTests()
        {
            void DoNothing()
            {
                Assert.True(true, "Local functions can be called.");
            }
            int Add(int x, int y)
            {
                return x + y;
            }

            void Multiply(int x, int y, out int res)
            {
                res = x * y;
            }

            DoNothing();

            Assert.AreEqual(40, Add(15, 25), "Local function returning a value works.");

            int multiRes;
            Multiply(20, 2, out multiRes);

            Assert.AreEqual(40, multiRes, "Local function with 'out' parameter works.");
        }
    }
}