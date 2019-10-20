using NUnit.Framework;
using ConfigurationBinder.Extensions.Parsers;

namespace ConfigurationBinder.Tests.Parsers
{
    [TestFixture]
    public class ArrayParserTests
    {
        [SetUp]
        protected void SetUp()
        {

        }

        [Test]
        public void ParseCommaSeparatedInts()
        {
            var parser = new ArrayParser(',', typeof(int));
            var text = "1,2,3,4";
            var expected = new [] {1, 2, 3, 4};

            var result = parser.Parse(text);

            Assert.AreEqual(expected, result);
        }
    }
}