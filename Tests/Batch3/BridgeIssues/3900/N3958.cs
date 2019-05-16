using Bridge.Test.NUnit;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3958 - {0}")]
    public class Bridge3958
    {
        [Test]
        public static void TestEnumStringOptional()
        {
            Assert.AreEqual("euclidean", GetOptions(MetricOptions.Euclidean));
            Assert.AreEqual("euclidean", GetOptions());
        }

        static object GetOptions(MetricOptions metric = MetricOptions.Euclidean) => metric;

        [Enum(Emit.StringNameLowerCase)]
        enum MetricOptions
        {
            Euclidean
        }
    }
}