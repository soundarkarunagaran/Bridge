using Bridge.Test.NUnit;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3865 - {0}")]
    public class Bridge3865
    {
        [Test]
        public static void TestArrayAssembly()
        {
            Assert.AreEqual(typeof(string).Assembly.FullName, typeof(string[][]).Assembly.FullName);
        }
    }
}