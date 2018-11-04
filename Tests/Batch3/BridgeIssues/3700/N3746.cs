using Bridge.Test.NUnit;
using System;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3746 - {0}")]
    public class Bridge3746
    {
        public class MyClass
        {
        }

        [Test]
        public static void TestAssemblyName()
        {
            Assert.AreEqual("mscorlib", typeof(Object).Assembly.FullName);
            Assert.AreEqual("Bridge.ClientTest.Batch3", typeof(MyClass).Assembly.FullName);
            Assert.AreEqual("Bridge.ClientTest.Batch3", typeof(MyClass[]).Assembly.FullName);
        }
    }
}