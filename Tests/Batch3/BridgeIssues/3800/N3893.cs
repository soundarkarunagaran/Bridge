using Bridge.Test.NUnit;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3893 - {0}")]
    public class Bridge3893
    {
        [External]
        [Reflectable]
        [Name("Bridge3893_TestClass")]
        public class TestClass
        {
#pragma warning disable CS0824
            public extern TestClass();
#pragma warning restore CS0824

            [Name("ctor_1")]
            public extern TestClass(string str);

            public int prop;
        }

        [Test]
        public static void TestCtorName()
        {
            /*@
              var Bridge3893_TestClass = function () {
                   this.prop = 1;
              };
              Bridge3893_TestClass.ctor_1 = function () {
                   this.prop = 2;
              };
             @*/

            var c1 = new TestClass();
            Assert.AreEqual(1, c1.prop);

            var c2 = new TestClass("");
            Assert.AreEqual(2, c2.prop);
        }
    }
}