using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3828 - {0}")]
    public class Bridge3828
    {
        [Test]
        public static void TestSortedList()
        {
            var list = new SortedList<int, string>();
            list[1] = "value1";

            Assert.AreEqual(list[1], "value1");
        }
    }
}