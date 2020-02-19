using ConfigurationBinder.Exceptions;
using ConfigurationBinder.Parsers;
using NUnit.Framework;

namespace ConfigurationBinder.Tests.Parsers
{
    [TestFixture]
    public class DefaultParserTests
    {
        [Test]
        public void ParseInt()
        {
            var parser = new DefaultParser(typeof(int));
            
            Assert.AreEqual(parser.Parse("10"), 10);
        }

        [Test]
        public void ParseNegativeInt()
        {
            var parser = new DefaultParser(typeof(int));

            Assert.AreEqual(parser.Parse("-10"), -10);
        }

        [Test]
        public void ParseDecimal()
        {
            var parser = new DefaultParser(typeof(decimal));

            Assert.AreEqual(parser.Parse("1.2"), 1.2m);
        }

        [Test]
        public void ParseNegativeDecimal()
        {
            var parser = new DefaultParser(typeof(decimal));

            Assert.AreEqual(parser.Parse("-1.2"), -1.2m);
        }

        [Test]
        public void ParseString()
        {
            var parser = new DefaultParser(typeof(string));
            Assert.AreEqual(parser.Parse("teststring"), "teststring");
        }

        [Test]
        public void ParseFloat()
        {
            var parser = new DefaultParser(typeof(float));
            Assert.AreEqual(parser.Parse("3.5"), 3.5F);
        }

        [Test]
        public void ParseNegativeFloat()
        {
            var parser = new DefaultParser(typeof(float));
            Assert.AreEqual(parser.Parse("-3.5"), -3.5F); 
        }

        [Test]
        public void ThrowOnBadInput()
        {
            var targetType = typeof(float);
            var parser = new DefaultParser(targetType);
            var input = "abc";

            var ex = Assert.Throws<ParsingException>(() => parser.Parse(input));
            Assert.That(ex.Input, Is.EqualTo(input));
            Assert.That(ex.TargetType, Is.EqualTo(targetType)); 
        }
    }
}