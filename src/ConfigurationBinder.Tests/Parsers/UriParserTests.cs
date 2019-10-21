using System;
using NUnit.Framework;
using UriParser = ConfigurationBinder.Extensions.Parsers.UriParser;

namespace ConfigurationBinder.Tests.Parsers
{
    [TestFixture]
    public class UriParserTests
    {
        [Test]
        public void ParsesAbsoluteUri()
        {
            var parser = new UriParser();
            var input = "https://google.com";

            var expected = new Uri(input);
            var output = parser.Parse(input);
            Assert.AreEqual(expected, output);
        }
    }
}