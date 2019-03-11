using Bridge.Test.NUnit;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3855 - {0}")]
    public class Bridge3855
    {
        [Test]
        public static void TestTypeName()
        {
            Assert.AreEqual("System.Type", typeof(Type).FullName);
            Assert.AreEqual("Type", typeof(Type).Name);

            Assert.AreEqual("System.Boolean", typeof(Boolean).FullName);
            Assert.AreEqual("Boolean", typeof(Boolean).Name);

            Assert.AreEqual("System.String", typeof(String).FullName);
            Assert.AreEqual("String", typeof(String).Name);

            Assert.AreEqual("System.DateTime", typeof(DateTime).FullName);
            Assert.AreEqual("DateTime", typeof(DateTime).Name);
        }
    }
}