using Bridge.Test.NUnit;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3876 - {0}")]
    public class Bridge3876
    {
        public interface IAbstr<out T>
        {
            int SomeProperty { get; }
        }

        class Impl : IAbstr<string>
        {
            public int SomeProperty { get; set; }
        }

        public class Fac
        {
            public static int CreateCalledCount = 0;

            public IAbstr<string> Create()
            {
                CreateCalledCount++;
                return new Impl();
            }
        }

        [Test]
        public static void TestVarianceMembers()
        {
            var fac = new Fac();
            int a = fac.Create().SomeProperty;
            Assert.AreEqual(1, Fac.CreateCalledCount);
        }
    }
}