using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3795 - {0}")]
    public class Bridge3795
    {
        [Reflectable]
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        public class DynamicPropertyAttribute : Attribute
        {
        }

        [Reflectable]
        public class RootViewItem
        {
            [DynamicProperty]
            public virtual bool ReadOnly { get; set; }
        }

        [Reflectable]
        public class PropertyViewItem : RootViewItem
        {
            public override bool ReadOnly { get; set; }
        }

        [Reflectable]
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        public class DynamicProperty2Attribute : Attribute
        {
        }

        [Reflectable]
        public class RootViewItem2
        {
            [DynamicProperty2]
            public virtual bool ReadOnly { get; set; }
        }

        [Reflectable]
        public class PropertyViewItem2 : RootViewItem2
        {
            [DynamicProperty2]
            public override bool ReadOnly { get; set; }
        }

        [Test]
        public static void TestReflection()
        {
            var type = typeof(PropertyViewItem);
            var property = type.GetProperty("ReadOnly");
            Assert.AreEqual("ReadOnly", property.Name);
            Assert.AreEqual(typeof(PropertyViewItem), property.DeclaringType);

            var attr = property.GetCustomAttributes().OfType<DynamicPropertyAttribute>().FirstOrDefault();
            Assert.NotNull(attr);

            type = typeof(PropertyViewItem);
            var properties = type.GetProperties();
            Assert.AreEqual(1, properties.Length);
            Assert.AreEqual("ReadOnly", properties[0].Name);
            Assert.AreEqual(typeof(PropertyViewItem), properties[0].DeclaringType);

            attr = properties[0].GetCustomAttributes().OfType<DynamicPropertyAttribute>().FirstOrDefault();
            Assert.NotNull(attr);

            type = typeof(PropertyViewItem2);
            properties = type.GetProperties();
            Assert.AreEqual(1, properties.Length);
            Assert.AreEqual("ReadOnly", properties[0].Name);
            Assert.AreEqual(typeof(PropertyViewItem2), properties[0].DeclaringType);

            var attrs = properties[0].GetCustomAttributes().OfType<DynamicProperty2Attribute>();
            Assert.AreEqual(2, attrs.Count());
        }
    }
}