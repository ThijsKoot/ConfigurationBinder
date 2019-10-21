using NUnit.Framework;
using ConfigurationBinder.Extensions.Parsers;
using ConfigurationBinder.Extensions.Exceptions;

namespace ConfigurationBinder.Tests.Parsers
{
    [TestFixture]
    public class ArrayParserTests
    {
        [Test]
        public void ParseCommaSeparatedInts()
        {
            var parser = new ArrayParser(',', typeof(int));
            var input = "1,2,3,4";
            var expected = new[] { 1, 2, 3, 4 };

            var result = parser.Parse(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ParseCommaSeparatedDecimal()
        {
            var parser = new ArrayParser(',', typeof(decimal));
            var input = "1.2, 3.4, 4.893";
            var expected = new decimal[] { 1.2m, 3.4m, 4.893m };

            var result = parser.Parse(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ThrowParsingExceptionOnMixedInput()
        {
            var parser = new ArrayParser(',', typeof(int));

            var input = "1,abc,3,4";
            var targetType = typeof(int[]);

            
            var ex = Assert.Throws<ParsingException>(() => parser.Parse(input));
            
            Assert.That(ex.Input, Is.EqualTo(input));
            Assert.That(ex.TargetType, Is.EqualTo(targetType)); 
        }
    }
}