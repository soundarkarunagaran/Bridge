using Bridge.Test.NUnit;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3388 - {0}")]
    public class Bridge3388
    {
        public class MyClass<T> : IFace<T>, IFace<List<T>>
        {
            string IFace<T>.Method() => "single";
            string IFace<List<T>>.Method() => "list";
        }

        public interface IFace<T>
        {
            string Method();
        }

        [Test]
        public static void TestTwoInterfaceImplementation()
        {
            var c = new MyClass<int>();
            Assert.AreEqual("single", ((IFace<int>)c).Method());
            Assert.AreEqual("list", ((IFace<List<int>>)c).Method());
        }
    }
}