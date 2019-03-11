using System;
using System.Text.RegularExpressions;
using Bridge.Html5;
using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#2522 - {0}")]
    public class Bridge2522
    {
        [Test]
        public static void TestIsPrimitive()
        {
            Assert.True(typeof(Byte).IsPrimitive);
            Assert.True(typeof(SByte).IsPrimitive);
            Assert.True(typeof(Int16).IsPrimitive);
            Assert.True(typeof(UInt16).IsPrimitive);
            Assert.True(typeof(Int32).IsPrimitive);
            Assert.True(typeof(UInt32).IsPrimitive);
            Assert.True(typeof(Int64).IsPrimitive);
            Assert.True(typeof(UInt64).IsPrimitive);
            Assert.True(typeof(Boolean).IsPrimitive);
            Assert.True(typeof(Char).IsPrimitive);
            Assert.True(typeof(Double).IsPrimitive);
            Assert.True(typeof(Single).IsPrimitive);

            Assert.False(typeof(Decimal).IsPrimitive);
            Assert.False(typeof(DateTime).IsPrimitive);
            Assert.False(typeof(String).IsPrimitive);
        }
    }
}