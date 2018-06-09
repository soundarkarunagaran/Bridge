using Bridge.Html5;
using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3625 - {0}")]
    public class Bridge3625
    {
        [Test]
        public static void TestLocalFns()
        {
            string One(string msg)
            {
                return msg;
            }

            string Two(string msg)
            {
                return "Two:" + One(msg);
            }

            Assert.AreEqual("One", One("One"));
            Assert.AreEqual("Two:One", Two("One"));
        }
    }
}