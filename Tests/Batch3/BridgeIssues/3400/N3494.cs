using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#3494 - {0}")]
    public class Bridge3494
    {
        [External]
        [Name("Bridge3494_A")]
        public class A
        {
            public static int InstancesCount { get; }
        }

        public class B : A
        {
        }

        [Init(InitPosition.Top)]
        public static void Init()
        {
            /*@
    var Bridge3494_A = (function () {
        function Bridge3494_A(s) {
            Bridge3494_A.InstancesCount++;
        }
        Bridge3494_A.InstancesCount = 0;
        return Bridge3494_A;
    }());
            */
        }

        [Test]
        public static void TestExtrenalClasCtor()
        {
            Assert.AreEqual(0, A.InstancesCount);

            var a = new A();
            Assert.AreEqual(1, A.InstancesCount);
            Assert.True(((object)a) is A);

            for (var i = 0; i < 10; i++)
            {
                var b = new B();
                Assert.True(((object)b) is A);
            }
            Assert.AreEqual(11, A.InstancesCount);
        }
    }
}