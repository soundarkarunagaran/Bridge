using Bridge.Test.NUnit;
using System;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [Category(Constants.MODULE_ISSUES)]
    [TestFixture(TestNameFormat = "#3432 - {0}")]
    public class Bridge3432
    {
        public interface ISome1
        {
            TimeSpan? TestTime { get; }
        }

        public class Some1 : ISome1
        {
            private string testTime;

            public string TestTime
            {
                get
                {
                    return this.testTime;
                }
                set
                {
                    this.testTime = value;
                }
            }

            TimeSpan? ISome1.TestTime
            {
                get
                {
                    return this.Parse(this.TestTime);
                }
            }

            private TimeSpan? Parse(string text)
            {
                if (text == null)
                    return null;

                string[] values = text.Split(':');

                if (values.Length == 2)
                    return new TimeSpan(0, 0, int.Parse(values[0]), int.Parse(values[1]));

                if (values.Length == 3)
                    return new TimeSpan(0, int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));

                throw new FormatException("Unsupported format of TimeSpan.");
            }
        }

        public interface ISome2 : ISome1
        {
            string Additional { get; }
        }

        public class Some2 : Some1, ISome2
        {
            public string Additional { get; set; }
        }

        [Test]
        public static void TestDerivation()
        {
            ISome1 some2 = new Some2() { TestTime = "00:02:00" };

            Assert.AreEqual(0, some2.TestTime?.Days);
            Assert.AreEqual(0, some2.TestTime?.Hours);
            Assert.AreEqual(2, some2.TestTime?.Minutes);
            Assert.AreEqual(0, some2.TestTime?.Seconds);
        }
    }
}