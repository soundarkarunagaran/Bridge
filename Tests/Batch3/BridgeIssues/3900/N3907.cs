using Bridge.Test.NUnit;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3907 - {0}")]
    public class Bridge3907
    {
        public interface I
        {
            // this unnecessary declaration causes the problem
            string ToString();
        }

        public class A : I
        {
            public override string ToString()
            {
                return "A";
            }
        }

        public class B : A, I
        {
        }

        [Test]
        public static void TestInterfaceToString()
        {
            I i = new B();

            Assert.AreEqual("A", i.ToString());
        }
    }
}