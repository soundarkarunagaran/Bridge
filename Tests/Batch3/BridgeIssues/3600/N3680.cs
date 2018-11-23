using Bridge.Test.NUnit;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3680 - {0}")]
    public class Bridge3680
    {
        [Reflectable]
        public interface ITest
        {
            string A { get; set; }
        }

        [Reflectable]
        public class Test
        {
            private Test() { }
            string A { get; set; }
        }

        [Test]
        public static void TestActivator()
        {
            Assert.Throws<MissingMethodException>(() => Activator.CreateInstance(typeof(Test)));
            Assert.Throws<MissingMethodException>(() => Activator.CreateInstance(typeof(ITest)));

            var test = Activator.CreateInstance(typeof(Test), true);

            Assert.NotNull(test);
            Assert.True(test is Test);
        }
    }
}