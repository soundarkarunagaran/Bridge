using Bridge.Html5;
using Bridge.Test.NUnit;
using System;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3934 - {0}")]
    public class Bridge3934
    {
        [Test]
        public static void TestConvert()
        {
            TestFloat();
            TestDouble();
            TestDecimal();
        }

        public static void TestFloat()
        {
            var oldCulture = System.Globalization.CultureInfo.CurrentCulture;

            try
            {
                float back;
                float input = 7.5f;
                System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
                string str = input.ToString();
                back = Convert.ToSingle(str);
                Assert.AreEqual(input, back, "Convert back to float using InvariantCulture");

                System.Globalization.CultureInfo norwegian = System.Globalization.CultureInfo.GetCultureInfo("nb-NO");
                str = input.ToString(norwegian);
                back = Convert.ToSingle(str, norwegian);
                Assert.AreEqual(input, back, "Convert back to float using Norwegian");


                System.Globalization.CultureInfo.CurrentCulture = norwegian;

                str = input.ToString();
                back = Convert.ToSingle(str);
                Assert.AreEqual(input, back, "Convert back to float using Norwegian as CurrentCulture");
            }
            finally
            {
                System.Globalization.CultureInfo.CurrentCulture = oldCulture;
            }            
        }

        public static void TestDouble()
        {
            var oldCulture = System.Globalization.CultureInfo.CurrentCulture;

            try
            {
                double back;
                double input = 7.5;
                System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
                string str = input.ToString();
                back = Convert.ToDouble(str);
                Assert.AreEqual(input, back, "Convert back to double using InvariantCulture");

                System.Globalization.CultureInfo norwegian = System.Globalization.CultureInfo.GetCultureInfo("nb-NO");
                str = input.ToString(norwegian);
                back = Convert.ToDouble(str, norwegian);
                Assert.AreEqual(input, back, "Convert back to double using Norwegian");


                System.Globalization.CultureInfo.CurrentCulture = norwegian;

                str = input.ToString();
                back = Convert.ToDouble(str);
                Assert.AreEqual(input, back, "Convert back to double using Norwegian as CurrentCulture");
            }
            finally
            {
                System.Globalization.CultureInfo.CurrentCulture = oldCulture;
            }            
        }

        public static void TestDecimal()
        {
            var oldCulture = System.Globalization.CultureInfo.CurrentCulture;

            try
            {
                decimal back;
                decimal input = 7.5m;
                System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
                string str = input.ToString();
                back = Convert.ToDecimal(str);
                Assert.AreEqual(input, back, "Convert back to decimal using InvariantCulture");

                System.Globalization.CultureInfo norwegian = System.Globalization.CultureInfo.GetCultureInfo("nb-NO");
                str = input.ToString(norwegian);
                back = Convert.ToDecimal(str, norwegian);
                Assert.AreEqual(input, back, "Convert back to decimal using Norwegian");


                System.Globalization.CultureInfo.CurrentCulture = norwegian;

                str = input.ToString();
                back = Convert.ToDecimal(str);
                Assert.AreEqual(input, back, "Convert back to decimal using Norwegian as CurrentCulture");
            }
            finally
            {
                System.Globalization.CultureInfo.CurrentCulture = oldCulture;
            }            
        }
    }
}