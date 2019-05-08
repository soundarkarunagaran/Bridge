using Bridge.Test.NUnit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3885 - {0}")]
    public class Bridge3885
    {
        public class Test : IEnumerable<int>, IEnumerable<string>
        {
            public int Max { get; }

            public Test(int max)
            {
                Max = max;
            }

            public IEnumerator<int> GetEnumerator() => new IntEnumerator(this);

            IEnumerator<string> IEnumerable<string>.GetEnumerator() => new StringEnumerator(this);

            IEnumerator IEnumerable.GetEnumerator() => new IntEnumerator(this);

            class IntEnumerator : IEnumerator<int>
            {
                private readonly int _max;
                public IntEnumerator(Test test)
                {
                    _max = test.Max;
                    Current = -1;
                }

                public int Current { get; set; }
                object IEnumerator.Current => Current;

                public void Dispose() { }
                public void Reset() { Current = -1; }

                public bool MoveNext() => ++Current <= _max;
            }

            class StringEnumerator : IEnumerator<string>
            {
                private readonly int _max;
                private int _index;
                public StringEnumerator(Test test)
                {
                    _max = test.Max;
                    _index = -1;
                    Current = "";
                }

                public string Current { get; set; }
                object IEnumerator.Current => Current;

                public void Dispose() { }
                public void Reset() { Current = ""; _index = -1; }

                public bool MoveNext()
                {
                    Current = "s" + ++_index;
                    return _index < _max;
                }
            }
        }

        [Test]
        public static void TestLinqEnumeration()
        {
            {
                var test = new Test(1);

                foreach (var i in test)
                {
                    Assert.True((object)i is int);
                }

                foreach (var i in (IEnumerable<string>)test)
                {
                    Assert.True((object)i is string);
                }

                foreach (var i in ((IEnumerable<string>)test).Select(d => d))
                {
                    Assert.True((object)i is string);
                }
            }

            {
                IEnumerable<string> test = new Test(1);
                foreach (var i in test)
                {
                    Assert.True((object)i is string);
                }
                foreach (var i in test.Select(d => d))
                {
                    Assert.True((object)i is string);
                }
            }
        }
    }
}