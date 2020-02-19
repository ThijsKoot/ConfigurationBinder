using ConfigurationBinder.Extensions;
using ConfigurationBinder.Parsers;
using NUnit.Framework;

namespace ConfigurationBinder.Tests.Parsers
{
    [TestFixture]
    public class ParserFactoryTests
    {
        [Test]
        public void GetArrayParserForArray()
        {
            var elementType = typeof(int);
            var arrayType = elementType.MakeArrayType();
            var separator = ConfigurationBinderOptions.Default.ArraySeparator;
            var expected = new ArrayParser(separator, elementType);

            var result = ParserFactory.CreateParser(arrayType) as ArrayParser;

            Assert.NotNull(result);
        }
    }
}