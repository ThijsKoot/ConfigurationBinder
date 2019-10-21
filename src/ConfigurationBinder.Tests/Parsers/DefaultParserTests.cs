using ConfigurationBinder.Extensions.Parsers;
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
            Assert.AreEqual(parser.Parse("-10"), -10);
        }
    }
}