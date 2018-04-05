using Bridge.Html5;
using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3513 - {0}")]
    public class Bridge3513
    {
        static void Run(Action a)
        {
            a();
        }

        [Test]
        public static void TestExtensionMethodAsAction()
        {
            Run("Hello, World!".Print);
        }
    }

    public static class Bridge3513SomeEx
    {
        public static void Print(this string message)
        {
            Assert.AreEqual("Hello, World!", message);
        }
    }
}