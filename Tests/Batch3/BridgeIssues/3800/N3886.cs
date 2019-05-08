using Bridge.Test.NUnit;
using System;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3886 - {0}")]
    public class Bridge3886
    {
        public class TestNoTpl
        {
            public TimeSpan One;
            public TimeSpan Two;
            public TimeSpan Three;
        }

        public class Test
        {
            public TimeSpan One
            {
                [Template("System.TimeSpan.parse({this}.One)")]
                get;
                [Template("{this}.One = Bridge.toString({value})")]
                set;
            }

            public TimeSpan Two
            {
                [Template("System.TimeSpan.parse({this}.Two)")]
                get;
                [Template("{this}.Two = Bridge.toString({value})")]
                set;
            }

            public TimeSpan Three
            {
                [Template("System.TimeSpan.parse({this}.Three)")]
                get;
                [Template("{this}.Three = Bridge.toString({value})")]
                set;
            }
        }

        public class TestSimpler
        {
            public TimeSpan One
            {
                get;
                [Template("{this}.One = Bridge.toString({value})")]
                set;
            }

            public TimeSpan Two
            {
                get;
                [Template("{this}.Two = Bridge.toString({value})")]
                set;
            }

            public TimeSpan Three
            {
                get;
                [Template("{this}.Three = Bridge.toString({value})")]
                set;
            }
        }

        [Test]
        public static void TestTemplateObjectInitializer()
        {
            var test = new TestNoTpl()
            {
                One = TimeSpan.FromDays(1),
                Two = TimeSpan.FromDays(2),
                Three = TimeSpan.FromDays(3)
            };

            Assert.True(TimeSpan.FromDays(1) == test.One);
            Assert.True(TimeSpan.FromDays(2) == test.Two);
            Assert.True(TimeSpan.FromDays(3) == test.Three);

            var test1 = new Test()
            {
                One = TimeSpan.FromDays(1),
                Two = TimeSpan.FromDays(2),
                Three = TimeSpan.FromDays(3)
            };

            Assert.True(TimeSpan.FromDays(1) == test1.One);
            Assert.True(TimeSpan.FromDays(2) == test1.Two);
            Assert.True(TimeSpan.FromDays(3) == test1.Three);

            var test2 = new Test();
            test2.One = TimeSpan.FromDays(1);
            test2.Two = TimeSpan.FromDays(2);
            test2.Three = TimeSpan.FromDays(3);

            Assert.True(TimeSpan.FromDays(1) == test2.One);
            Assert.True(TimeSpan.FromDays(2) == test2.Two);
            Assert.True(TimeSpan.FromDays(3) == test2.Three);

            var test3 = new TestSimpler()
            {
                One = TimeSpan.FromDays(1),
                Two = TimeSpan.FromDays(2),
                Three = TimeSpan.FromDays(3)
            };

            Assert.AreEqual("1.00:00:00", test3.One);
            Assert.AreEqual("2.00:00:00", test3.Two);
            Assert.AreEqual("3.00:00:00", test3.Three);
        }
    }
}