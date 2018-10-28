using Bridge.Test.NUnit;
using System.Threading.Tasks;
using static Bridge.ClientTest.Batch3.BridgeIssues.Bridge3742Static.dom;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    public static class Bridge3742Static
    {
        public static class dom
        {
            public class HTMLSpanElement1
            {

            }
        }
    }

    [TestFixture(TestNameFormat = "#3742 - {0}")]
    public class Bridge3742
    {
        public class MyClass<T>
        {
            public MyClass()
            {

            }
        }
        public class ClassName
        {
            public static MyClass<T>[] Fetch<T>(MyClass<T>[] objects)
            {
                //code here
                return new MyClass<T>[10];
            }
        }

        [Test]
        public static void TestGenericUsingStatic()
        {
            var objects = new MyClass<HTMLSpanElement1>[3];

            var result = ClassName.Fetch(objects);
            Assert.NotNull(result);
        }
    }
}