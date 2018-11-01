using Bridge.Test.NUnit;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3679 - {0}")]
    public class Bridge3679
    {
        [Test]
        public static void TestStringIndexOf()
        {
            var msg = "Hello, World!";

            int i = msg.IndexOf("world", 3, StringComparison.InvariantCultureIgnoreCase);

            Assert.AreEqual(7, i);
        }
    }
}