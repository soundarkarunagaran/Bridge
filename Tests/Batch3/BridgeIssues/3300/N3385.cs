using Bridge.Html5;
using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3385 - {0}")]
    public class Bridge3385
    {
        [Test]
        public static void TestObjectToChar()
        {
            object a = 'a';
            Assert.AreEqual('a', Convert.ToChar(a));
        }
    }
}