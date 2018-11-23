using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3729 - {0}")]
    public class Bridge3729
    {
        public interface Interface1
        {
            [Rules(AutoProperty = AutoPropertyRule.Plain)]
            string Name { get; set; }
        }

        public interface Interface2 : Interface1
        {

        }

        public interface Interface3 : Interface1
        {

        }

        public class Class1<T> : Interface1
        {
            [Rules(AutoProperty = AutoPropertyRule.Plain)]
            public string Name { get; set; }
        }

        public class Class2 : Class1<string>, Interface1, Interface2, Interface3
        {

        }

        [Test]
        public static void TestAliases()
        {
            var c2 = new Class2();
            Class1<string> c1 = c2;
            Interface2 i2 = c2;
            Interface3 i3 = c2;
            Interface1 i1 = c2;

            c1.Name = "test";

            Assert.AreEqual("test", c2.Name);
            Assert.AreEqual("test", i1.Name);
            Assert.AreEqual("test", i2.Name);
            Assert.AreEqual("test", i3.Name);
        }
    }
}