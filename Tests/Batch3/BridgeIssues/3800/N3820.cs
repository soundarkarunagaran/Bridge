using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3820 - {0}")]
    public class Bridge3820
    {
        class Score
        {
            public int value = 5;

            public ref int Get()
            {
                return ref this.value;
            }
        }

        public delegate ref result GetRef<result>();
        public static c1 a = new c1();

        public class c1
        {
            public string[] a = new string[] { "a", "b" };
        }

        public static void Set(ref int m)
        {
            m = 11;
        }

        public static void Set1(int m)
        {
            m = 11;
        }

        [Test]
        public static void TestRefReturn()
        {
            GetRef<c1> refa = () => ref a;
            refa().a[0] = "c";

            Assert.AreEqual("c", refa().a[0]);

            var score = new Score();
            score.Get() = 10;
            Assert.AreEqual(10, score.Get());

            var s = score.Get();

            int i = 1;
            Set(ref i);
            Assert.AreEqual(11, i);

            Set(ref s);
            Assert.AreEqual(11, s);
            Assert.AreEqual(10, score.Get());

            Set(ref score.Get());
            Assert.AreEqual(11, score.Get());

            score.Get() = 10;
            Set1(score.Get());
            Assert.AreEqual(10, score.Get());
        }
    }
}