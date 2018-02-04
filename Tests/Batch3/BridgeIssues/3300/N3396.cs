using Bridge.Test.NUnit;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3396 - {0}")]
    public class Bridge3396
    {
        private struct TestStructure
        {
            public bool Foo { set; get; }
        }

        [ObjectLiteral]
        private struct TestStructure2
        {
            public bool Foo { set; get; }
        }

        [Test]
        public static void TestMultiDimArrayDefValue()
        {
            var map2d = new TestStructure[5, 5];
            map2d[1, 1].Foo = true;
            Assert.True(map2d[1, 1].Foo);
            Assert.False(map2d[2, 2].Foo);
        }

        [Test]
        public static void TestMultiDimArrayDefValue2()
        {
            var map2d = new TestStructure2[5, 5];
            map2d[1, 1].Foo = true;
            Assert.True(map2d[1, 1].Foo);
            Assert.Null(map2d[2, 2].Foo);
        }
    }
}