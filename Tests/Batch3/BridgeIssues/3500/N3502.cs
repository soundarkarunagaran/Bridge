using Bridge.Html5;
using Bridge.Test.NUnit;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3502 - {0}")]
    public class Bridge3502
    {
        public class BaseClass
        {
            public int Value { get; }
            public BaseClass(int i)
            {
                Value = i;
            }
        }

        public class SubClass : BaseClass
        {
            public SubClass(int i) : base(i)
            {
            }
        }

        public interface ITestInterface<T>
        {
            int TestCount { get; }
        }

        public interface ITestReadValue<out T>
        {
            T Value { get; }
        }

        public class TestList<T> : List<T>, ITestInterface<T>, ITestReadValue<IReadOnlyList<T>>
        {
            public int TestCount { get; set; }
            public IReadOnlyList<T> Value => this;

            public TestList(IEnumerable<T> items)
            {
                AddRange(items);
                TestCount = Count;
            }
        }

        public static ITestInterface<SubClass> GetList()
        {
            return new TestList<SubClass>(Enumerable.Range(1, 10).Select(i => new SubClass(i)));
        }

        [Test(ExpectedCount = 10)]
        public static void TestInvariance()
        {
            ITestInterface<SubClass> list = GetList();

            var readable = (ITestReadValue<IReadOnlyList<BaseClass>>)list;

            int i = 0;
            foreach (var item in readable.Value)
            {
                Assert.AreEqual(++i, item.Value);
            }
        }
    }
}