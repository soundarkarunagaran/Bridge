using Bridge.Test.NUnit;
using System;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3373 - {0}")]
    public class Bridge3373
    {
        [Reflectable]
        public class MyTypeClass
        {
            [Reflectable]
            public class MyClass1
            {
            }

            [Reflectable]
            public class MyClass2
            {
            }

            [Reflectable]
            protected class MyClass3
            {
            }

            [Reflectable]
            protected class MyClass4
            {
            }
        }


        [Test]
        public static void TestGetNestedTypes()
        {
            Type myType = (typeof(MyTypeClass));
            Type[] myTypeArray = myType.GetNestedTypes(BindingFlags.Public);
            Assert.AreEqual(2, myTypeArray.Length);
            Assert.AreEqual(typeof(MyTypeClass.MyClass1), myTypeArray[0]);
            Assert.AreEqual(typeof(MyTypeClass.MyClass2), myTypeArray[1]);

            Type[] myTypeArray1 = myType.GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.AreEqual(2, myTypeArray1.Length);
            Assert.AreEqual("Bridge.ClientTest.Batch3.BridgeIssues.Bridge3373+MyTypeClass+MyClass3", myTypeArray1[0].FullName);
            Assert.AreEqual("Bridge.ClientTest.Batch3.BridgeIssues.Bridge3373+MyTypeClass+MyClass4", myTypeArray1[1].FullName);
        }

        [Test]
        public static void TestGetNestedType()
        {
            Type myType = (typeof(MyTypeClass));
            Type type = myType.GetNestedType("MyClass1");
            Assert.NotNull(type);
            Assert.AreEqual(typeof(MyTypeClass.MyClass1), type);

            type = myType.GetNestedType("MyClass3", BindingFlags.NonPublic);
            Assert.NotNull(type);
            Assert.AreEqual("Bridge.ClientTest.Batch3.BridgeIssues.Bridge3373+MyTypeClass+MyClass3", type.FullName);
        }
    }
}