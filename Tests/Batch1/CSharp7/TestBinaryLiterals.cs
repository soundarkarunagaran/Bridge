using Bridge.Test.NUnit;
using Stuff = System.ComponentModel;

namespace Bridge.ClientTest.CSharp6
{
    [Category(Constants.MODULE_BASIC_CSHARP)]
    [TestFixture(TestNameFormat = "Binary Literals - {0}")]
    public class TestBinaryLiterals
    {
        [Test]
        public static void TestBasic()
        {
            int c = 0B110010;
            int f = 0b110010 * 2;

            int binaryData = 0B0010_0111_0001_0000; // binary value of 10000   
            int hexaDecimalData = 0X2B_67; //HexaDecimal Value of 11,111   
            int decimalData = 104_567_789;
            int myCustomData = 1___________2__________3___4____5_____6;
            double realdata = 1_000.111_1e1_00;

            Assert.AreEqual(50, c);
            Assert.AreEqual(100, f);
            Assert.AreEqual(10000, binaryData);
            Assert.AreEqual(11111, hexaDecimalData);
            Assert.AreEqual(104567789, decimalData);
            Assert.AreEqual(123456, myCustomData);
            Assert.AreEqual(1.0001111E+103, realdata);
        }
    }
}