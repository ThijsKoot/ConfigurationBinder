using ConfigurationBinder.Extensions.Exceptions;
using ConfigurationBinder.Extensions.Parsers;
using NUnit.Framework;

namespace ConfigurationBinder.Tests.Parsers
{
    [TestFixture]
    public class EnumParserTests
    {
        enum FooBar
        {
            Foo,
            Bar
        }

        private EnumParser GetParser() => new EnumParser(typeof(FooBar));

        [Test]
        public void ParseEnumFromKey()
        {
            var parser = GetParser();

            var input = "Foo";
            var expected = FooBar.Foo;

            Assert.AreEqual(parser.Parse(input), expected);
        }

        [Test]
        public void ParseEnumFromKeyCaseInsensitive()
        {
            var parser = GetParser();

            var input = "Foo";
            var expected = FooBar.Foo;

            Assert.AreEqual(parser.Parse(input), expected);
        }

        [Test]
        public void ParseEnumFromValue()
        {
            var parser = GetParser();
            var input = "1";
            var expected = FooBar.Bar;

            Assert.AreEqual(parser.Parse(input), expected);
        }

        [Test]
        public void ThrowParsingExceptionOnBadInput()
        {
            var parser = GetParser();
            var input = "FooBar";
            
            var ex = Assert.Throws<ParsingException>(() => parser.Parse(input));
            Assert.That(Is.Equals(ex.TargetType, typeof(FooBar)));
            Assert.That(Is.Equals(ex.Input, input));
        }
    }
}