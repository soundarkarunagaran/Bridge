using Bridge.Test.NUnit;
using System;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3939 - {0}")]
    public class Bridge3939
    {
        [Reflectable]
        public class Test
        {
            public DateTime NonNullableProp { get; set; } = DateTime.Now;
            public DateTime NonNullableField = DateTime.Now;
        }

        [Test]
        public static void TestReflectionSetValue()
        {
            Test test = new Test();

            Assert.AreEqual(DateTime.Now.Year, test.NonNullableProp.Year);
            PropertyInfo propNonNullable = test.GetType().GetProperty("NonNullableProp");
            propNonNullable.SetValue(test, null);
            Assert.AreEqual(default(DateTime).Year, test.NonNullableProp.Year);

            test = new Test();
            Assert.AreEqual(DateTime.Now.Year, test.NonNullableField.Year);
            FieldInfo fieldNonNullable = test.GetType().GetField("NonNullableField");
            fieldNonNullable.SetValue(test, null);
            Assert.AreEqual(default(DateTime).Year, test.NonNullableField.Year);
        }
    }
}