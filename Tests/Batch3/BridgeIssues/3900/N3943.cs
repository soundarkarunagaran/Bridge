using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3943 - {0}")]
    public class Bridge3943
    {
        [Test]
        public static void TestStaticInitialization()
        {
            Assert.AreEqual("A", c2.p2.p1);
        }

        public class c1
        {
            public string p1 { get; set; }
        }

        public class c2
        {
            private static c1 _s = _init();

            private static c1 _init()
            {
                c1 o1 = new c1
                {
                    p1 = Z1
                    //works: p1 = "A"
                };

                return o1;
            }

            public static c1 p2 { get { return _s; } }

            public string p3 { get; set; }

            public const string Z1 = "A";
        }
    }
}