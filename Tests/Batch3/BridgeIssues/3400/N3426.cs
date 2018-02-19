using Bridge.Test.NUnit;
using System;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#3426 - {0}")]
    public class Bridge3426
    {
        public abstract class AbstractClass
        {
            public string Type { get { return GetType().FullName; } }
        }

        public class ConcreteClass : AbstractClass
        {
        }

        [Test]
        public static void TestGetTypeInAbstract()
        {
            ConcreteClass test = new ConcreteClass();
            Assert.AreEqual("Bridge.ClientTest.Batch3.BridgeIssues.Bridge3426+ConcreteClass", test.Type);
        }
    }
}