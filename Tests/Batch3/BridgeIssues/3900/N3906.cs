using Bridge.Test.NUnit;
using System.Collections.Generic;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3906 - {0}")]
    public class Bridge3906
    {
        public class X
        {
            public int v;
        }

        [Test]
        public static void TestAssigmentIncrement()
        {
            Queue<X> q = new Queue<X>();
            q.Enqueue(new X());
            q.Enqueue(new X());
            q.Dequeue().v += 7;
            Assert.AreEqual(1, q.Count);
        }
    }
}