using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3789 - {0}")]
    public class Bridge3789
    {
        public class c1<T2>
        {
            public IEnumerable<c2> f1(IEnumerable<string> coll1)
            {
                IEnumerable<c2> coll2 = coll1.Select(item_ =>
                new c2
                {
                    m1 = 1
                });

                return coll2;
            }
            public class c2
            {
                internal int m1 { get; set; }
            }
        }

        [Test]
        public static void TestLINQ()
        {
            var col = new c1<int>().f1(new string[1] { "" });

            Assert.AreEqual(1, col.Count());
            Assert.True(col.First() is c1<int>.c2);
            Assert.AreEqual(1, col.First().m1);
        }
    }
}