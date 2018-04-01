using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#3499 - {0}")]
    public class Bridge3499
    {
        [Test]
        public static void TestKeysWithSimilarHashCode()
        {
            Dictionary<Guid, string> superDict = new Dictionary<Guid, string>();

            
            var x1 = "str1";
            Guid guid1 = new Guid("00000003-0000-0000-0001-00001c000000");
            var x2 = "str2";
            Guid guid2 = new Guid("00000001-0000-0000-0001-000004000000");
            var x3 = "str3";
            Guid guid3 = new Guid("00000003-0000-0000-0001-000022000000");

            superDict.Add(guid1, x1);
            superDict.Add(guid2, x2);
            superDict.Add(guid3, x3);

            var keys = superDict.Keys;
            Assert.AreEqual(3, keys.Count());
            Assert.True(keys.Contains(guid1));
            Assert.True(keys.Contains(guid2));
            Assert.True(keys.Contains(guid3));

            var values = superDict.Values;
            Assert.AreEqual(3, values.Count());
            Assert.True(values.Contains(x1));
            Assert.True(values.Contains(x2));
            Assert.True(values.Contains(x3));
        }
    }
}