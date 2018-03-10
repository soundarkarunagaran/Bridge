using Bridge.Test.NUnit;
using System;
using System.Threading.Tasks;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#3453 - {0}")]
    public class Bridge3453
    {
        public class c0
        {
            public int I
            {
                get;
                set;
            }
        }

        public interface i1
        {
            Task<TResult> m1<TResult>(TResult result);
        }
        public class c1
        {
            public Task<TResult> m1<TResult>(TResult result)
            {
                return Task.FromResult<TResult>(result);
            }
        }
        public class c2 : c1, i1
        {
        }

        [Test]
        public static void TestDerivedGenericInterface()
        {
            i1 o1 = new c2();
            var c = new c0 { I = 16 };
            var x = o1.m1<c0>(c).Result;

            Assert.AreEqual(c, x);
        }
    }
}