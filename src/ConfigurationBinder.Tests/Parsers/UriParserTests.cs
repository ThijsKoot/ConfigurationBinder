using System;
using ConfigurationBinder.Extensions.Exceptions;
using NUnit.Framework;
using UriParser = ConfigurationBinder.Extensions.Parsers.UriParser;

namespace ConfigurationBinder.Tests.Parsers
{
    [TestFixture]
    public class UriParserTests
    {
        [Test]
        public void ParseAbsoluteUri()
        {
            var parser = new UriParser();
            var input = "https://google.com";

            var expected = new Uri(input);
            var output = parser.Parse(input);
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ParseFileUri()
        {
            var parser = new UriParser();
            var input = "file://C:\\foo.bar";
            var expected = new Uri(input);

            Assert.AreEqual(parser.Parse(input), expected);
        }

        [Test]
        public void ParseRelativeUri()
        {
            var parser = new UriParser();
            var input = "/foo/bar";
            var expected = new Uri(input, UriKind.Relative);

            Assert.AreEqual(parser.Parse(input), expected);
        }

        [Test]
        public void ThrowOnMalformedInput()
        {
            var parser = new UriParser();
            var input = "https:/google.com";
            var ex = Assert.Throws<ParsingException>(() => parser.Parse(input));

            Assert.That(ex.TargetType == typeof(Uri));
            Assert.That(ex.Input == input);
            Assert.That(ex.InnerException.GetType() == typeof(UriFormatException));
        }
    }
}