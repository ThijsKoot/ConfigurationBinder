using ConfigurationBinder.Extensions.Exceptions;
using ConfigurationBinder.Extensions.Parsers;
using NUnit.Framework;
using System;

namespace ConfigurationBinder.Tests.Parsers
{
    [TestFixture]
    public class GuidParserTests
    {
        [Test]
        public void ParseGuid()
        {
            var parser = new GuidParser();
            var input = "05da4492-054d-48f9-8b0e-a0e90c2aee4b";
            var inputWithoutDash = "05da4492054d48f98b0ea0e90c2aee4b";

            var expected = Guid.Parse(input);

            Assert.AreEqual(parser.Parse(input), expected);
            Assert.AreEqual(parser.Parse(inputWithoutDash), expected);
        }

        [Test]
        public void ThrowsParsingExceptionOnBadInput()
        {
            var parser = new GuidParser();
            var input = "notactuallyaguid";

            var ex = Assert.Throws<ParsingException>(() => parser.Parse(input));
            
            Assert.That(ex.Input, Is.EqualTo(input));
            Assert.That(ex.TargetType, Is.EqualTo(typeof(Guid))); 
        }
    }
}