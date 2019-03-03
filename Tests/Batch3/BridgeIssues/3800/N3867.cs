using Bridge.Test.NUnit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3867 - {0}")]
    public class Bridge3867
    {
        [Reflectable]
        public class MyModel
        {
            public int Prop { get; set; }
        }

        [Test]
        public static void TestBackingField()
        {
            var c = new MyModel { Prop = 1 };
            var t = typeof(MyModel);
            var fields = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.AreEqual(1, fields.Length);

            var field = fields[0];
            Assert.AreEqual("<Prop>k__BackingField", field.Name);
            Assert.AreEqual(1, field.GetValue(c));

            field.SetValue(c, 2);
            Assert.AreEqual(2, field.GetValue(c));
            Assert.AreEqual(2, c.Prop);
        }
    }
}