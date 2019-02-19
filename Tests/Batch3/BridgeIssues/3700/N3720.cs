using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3720 - {0}")]
    public class Bridge3720
    {
        [Flags]
        enum Test
        {
            A = 1 << 0,
            B = 1 << 1
        }

        [Test]
        public static void TestEnumToObject()
        {
            ulong v = 3;

            Test test = (Test)Enum.ToObject(typeof(Test), v);

            Assert.True(test.HasFlag(Test.A));
            Assert.True(test.HasFlag(Test.B));
        }
    }
}