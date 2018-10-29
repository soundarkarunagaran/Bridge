using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using static Bridge.ClientTest.Batch3.BridgeIssues.Bridge3467_Test1;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    sealed class Bridge3467_Test1 : Bridge3467_ITest<NestedClass>
    {
        public sealed class NestedClass { }
    }

    interface Bridge3467_ITest<T> { }

    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#3467 - {0}")]
    public class Bridge3467
    {
        [Test]
        public static void TestGenericUsingStatic()
        {
            Assert.NotNull(new Bridge3467_Test1());
        }
    }
}