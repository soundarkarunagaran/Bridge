using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#3485 - {0}")]
    public class Bridge3485
    {
        [External]
        [Name("Bridge3485_A")]
        public class A
        {
            public string V1 { get; }
            public virtual string V2 { get; }
        }

        public class B : A
        {
            public string GetV1() => base.V1;
            public string GetV2() => base.V2;
        }

        [Init(InitPosition.Top)]
        public static void Init()
        {
            /*@
    var Bridge3485_A = (function () {
        function A() {
            this.V1 = "value1";
            this.V2 = "value2";
        }
        return A;
    }());
            */
        }

        [Test]
        public static void TestExternalVirtualProperty()
        {
            var v1 = new B().GetV1();
            var v2 = new B().GetV2();

            Assert.AreEqual("value1", v1);
            Assert.AreEqual("value2", v2);
        }
    }
}