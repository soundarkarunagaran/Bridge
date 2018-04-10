using System;
using Bridge.Html5;
using Bridge.Test.NUnit;
using System.Collections.Generic;
using System.Linq;
using Bridge;
using ExternalNS3516;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3516 - {0}")]
    public class Bridge3516
    {
        [Test]
        [Rules(ExternalCast = ExternalCastRule.Plain)]
        public static void TestExternalCastRule()
        {
            object obj = "test";
            var el = (ExternalClass3516)obj;

            Assert.AreEqual("test", el);

            obj = new List<string>();
            Assert.Throws<InvalidCastException>(() => {
                obj = (IEqualityComparer<string>)obj;
            });
        }
    }
}

namespace ExternalNS3516
{
    [External]
    public class ExternalClass3516
    {
    }
}