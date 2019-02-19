using Bridge.Html5;
using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3809 - {0}")]
    public class Bridge3809
    {
        [Test]
        public static void TestIsAssignableFrom()
        {
            Assert.True(typeof(HTMLElement).IsAssignableFrom(typeof(HTMLDivElement)));
        }
    }
}