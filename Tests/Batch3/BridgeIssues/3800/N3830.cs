using Bridge.Test.NUnit;
using System;
using System.Collections.Generic;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3830 - {0}")]
    public class Bridge3830
    {
        [Test]
        public static void TestArrayResize()
        {
            String[] myArr = {"The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog"};
            Assert.AreEqual(8, myArr.GetUpperBound(0));

            Array.Resize(ref myArr, myArr.Length + 5);
            Assert.AreEqual(13, myArr.GetUpperBound(0));
            Assert.AreEqual("The", myArr[0]);
            Assert.AreEqual("quick", myArr[1]);
            Assert.AreEqual("brown", myArr[2]);
            Assert.AreEqual("fox", myArr[3]);
            Assert.AreEqual("jumps", myArr[4]);
            Assert.AreEqual("over", myArr[5]);
            Assert.AreEqual("the", myArr[6]);
            Assert.AreEqual("lazy", myArr[7]);
            Assert.AreEqual("dog", myArr[8]);
            Assert.AreEqual(null, myArr[9]);
            Assert.AreEqual(null, myArr[10]);
            Assert.AreEqual(null, myArr[11]);
            Assert.AreEqual(null, myArr[12]);
            Assert.AreEqual(null, myArr[13]);

            Array.Resize(ref myArr, 4);
            Assert.AreEqual(3, myArr.GetUpperBound(0));
            Assert.AreEqual("The", myArr[0]);
            Assert.AreEqual("quick", myArr[1]);
            Assert.AreEqual("brown", myArr[2]);
            Assert.AreEqual("fox", myArr[3]);
        }
    }
}