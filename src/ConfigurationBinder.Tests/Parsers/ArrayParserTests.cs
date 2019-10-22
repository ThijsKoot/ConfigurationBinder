using NUnit.Framework;
using ConfigurationBinder.Extensions.Parsers;
using ConfigurationBinder.Extensions.Exceptions;
using System;
using System.Linq;
using System.Collections;

namespace ConfigurationBinder.Tests.Parsers
{
    [TestFixture]
    public class ArrayParserTests
    {
        [Test]
        public void ParseCommaSeparatedIntArray()
        {
            var parser = new ArrayParser(',', typeof(int));
            var input = "1,2,3,4";
            int[] expected = { 1, 2, 3, 4 };

            var result = parser.Parse(input);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ParseCommaSeparatedDecimalArray()
        {
            var parser = new ArrayParser(',', typeof(decimal));
            var input = "1.2, 3.4, 4.893";
            decimal[] expected = { 1.2m, 3.4m, 4.893m };

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

        [Test]
        public void ThrowCorrectInnerException()
        {
            var parser = new ArrayParser(',', typeof(int));

            var input = "1,abc,3,4";
            var targetType = typeof(int[]);

            var ex = Assert.Throws<ParsingException>(() => parser.Parse(input));

            var innerException = ex.InnerException as ParsingException;
            Assert.NotNull(innerException);
            Assert.That(innerException.TargetType == typeof(int));
            Assert.That(innerException.Input == "abc");
        }

        [Test]
        public void ParseCommaSeparatedGuidArray()
        {
            var parser = new ArrayParser(',', typeof(Guid));
            string[] guids = 
            { 
                "af8cecf7-45ab-4666-9427-cfbd3ab34bb9", 
                "724f5817-7f9a-4219-9fbe-e83621e4a733", 
                "8bc122f8-fba8-43de-968c-ec335ea24b1d" 
            };

            var input = string.Join(",", guids);
            
            var expected = guids.Select(x => Guid.Parse(x)).ToArray();

            Assert.AreEqual(parser.Parse(input), expected);
        }
    }
}