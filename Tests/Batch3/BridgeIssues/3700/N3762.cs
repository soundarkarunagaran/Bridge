using Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.Test.NUnit;

//TODO: Issue3762 is fixed but Bridge.Test plugin generates wrong code for such test case. It is different issue and should be resolved separatelly
/*namespace SomeName3762
{
    public class A
    {
        public string P { get; set; }

        public A()
        {
            P = ""; //dotnetfiddle doesn't support property initializer
        }

        public override string ToString()
        {
            return P;
        }
    }

    public class B
    {
        public static string Fail<T>(T v, Func<T, string> transform)
        {
            return transform(v);
        }

        public static string Fail2<T>(Func<T, string> transform)
        {
            return transform(default(T));
        }

        public static string Fine<T>(T v)
        {
            return v.ToString();
        }

        public static A Helper(A aaa)
        {
            return aaa;
        }
    }

    [TestFixture(TestNameFormat = "#3762 - {0}")]
    public class SomeName3762
    {
        [Test]
        public static void TestModelResolving()
        {
            Assert.AreEqual("123456", B.Fail(new A { P = "123" }, x => x.P + "456")); //this fails in 17.4.0
            Assert.AreEqual("678", B.Fail2((A x) => x + "678")); //this fails in 17.4.0

            Assert.AreEqual("123456", B.Fail<A>(new A { P = "123" }, x => x.P + "456")); //workaround for 17.4.0
            Assert.AreEqual("678", B.Fail2<A>((A x) => x + "678")); //workaround for 17.4.0

            Assert.AreEqual("13", B.Fine(new A { P = "13" }));

            var lst = new List<A> { new A { P = "22" } };
            var lstFail = lst.Select(z => {
                return B.Helper(z);
            }).ToList(); //this fails in 17.4.0

            Assert.AreEqual(1, lstFail.Count);
            Assert.AreEqual("22", lstFail[0].P);

            var lstWorkaround = lst.Select<A, A>(z => {
                return B.Helper(z);
            }).ToList(); //workaround for 17.4.0

            Assert.AreEqual(1, lstWorkaround.Count);
            Assert.AreEqual("22", lstFail[0].P);
            Console.WriteLine("done");
        }
    }
}
*/