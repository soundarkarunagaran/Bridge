using Bridge.Test.NUnit;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3904 - {0}")]
    public class Bridge3904
    {
        [Test]
        public static void TestArrayCovariance()
        {
            object ao1 = new int[1];
            Assert.False(ao1 is object[]);

            var ao = new int[][] { new int[1] };
            Assert.True(ao is object[]);

            object a = new int[][][] { new int[][] { new int[1] } };
            Assert.True(a is object[]);
            Assert.True(a is object[][]);
            Assert.False(a is object[][][]);

            object b = new string[][] { new string[] { "hi" } };
            Assert.True(b is string[][]);
            Assert.True(b is object[]);
            Assert.True(b is object[][]);
            Assert.False(b is object[][][]);
        }

        public class c1
        {
        }

        public class c2 : c1
        {
        }

        public struct s1 { }

        [Test]
        public static void TestArrayCovariance2()
        {
            c2[] i2 = new c2[1];
            Assert.True(i2 is object[]);
            Assert.True(i2 is c1[]);

            object ao = new s1[1];
            Assert.False(ao is object[]);

            ao = new string[1];
            Assert.True(ao is object[]);

            ao = new int[1];
            Assert.False(ao is object[]);

            Assert.False(typeof(object[]).IsAssignableFrom(typeof(int[])));
            Assert.True(typeof(object[]).IsAssignableFrom(typeof(int[][])));
        }
    }
}