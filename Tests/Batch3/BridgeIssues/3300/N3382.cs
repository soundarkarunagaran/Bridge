using Bridge.Test.NUnit;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3382 - {0}")]
    public class Bridge3382
    {
        static int tag;

        public class BaseClass
        {
            public IList<string> Items;

            public BaseClass(params string[] items)
            {
                tag = 1;
                Items = items;
            }

            public BaseClass(IList<string> items)
            {
                tag = 2;
                Items = items;
            }
        }

        public class SubClassBrokenConstructorCall : BaseClass
        {
            public SubClassBrokenConstructorCall()
            {
            }
        }

        public class SubClassWorkAroundConstructorCall : BaseClass
        {
            public SubClassWorkAroundConstructorCall() : base(new string[0])
            {
            }
        }

        [Test]
        public static void TestBaseCtor()
        {
            tag = 0;
            new SubClassBrokenConstructorCall();
            Assert.AreEqual(1, tag);

            tag = 0;
            new SubClassWorkAroundConstructorCall();
            Assert.AreEqual(1, tag);
        }
    }
}