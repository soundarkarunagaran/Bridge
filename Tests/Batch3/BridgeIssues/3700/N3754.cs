using Bridge.Test.NUnit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3754 - {0}")]
    public class Bridge3754
    {
        public class Test : IEnumerable<(string Name, string Value)>
        {
            private readonly Dictionary<string, string> data = new Dictionary<string, string> { { "a1", "b1" }, { "a2", "b2" } };

            public IEnumerator<(string Name, string Value)> GetEnumerator()
            {
                foreach (KeyValuePair<string, string> pair in this.data)
                {
                    yield return (pair.Key, pair.Value);
                }
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        [Test]
        public static void TestGnericParameterValueTuple()
        {
            Test test = new Test();

            Assert.AreEqual("a1", test.First().Name);
            Assert.NotNull(test.Select(item => (object)item.Value));
            Assert.NotNull(test.Select(item => (object)item.Value).First());
        }
    }
}