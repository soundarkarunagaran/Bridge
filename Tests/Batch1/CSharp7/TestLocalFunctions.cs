using Bridge.Test.NUnit;
using System.Collections.Generic;

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
        [Test(ExpectedCount = 4)]
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

            LocalRetType AddCustomType(int x, int y)
            {
                return new LocalRetType() { Value = x + y };
            }

            void Multiply(int x, int y, out int res)
            {
                res = x * y;
            }

            DoNothing();

            Assert.AreEqual(40, Add(15, 25), "Local function returning a value works.");

            var customTypeResult = AddCustomType(1, 2);
            Assert.AreEqual(3, customTypeResult.Value, "Local function returning a custom type works.");

            int multiRes;
            Multiply(20, 2, out multiRes);
            Assert.AreEqual(40, multiRes, "Local function with 'out' parameter works.");
        }

        [Test]
        public static void TestArrowLocalFunction()
        {
            var numbers = new List<string>() { "one", "two" };

            var length = numbers.Count;

            string Length() => $"length is {length}";

            Assert.AreEqual("length is 2", Length());
        }

        public class LocalRetType
        {
            public int Value { get; set; }
        }
    }
}