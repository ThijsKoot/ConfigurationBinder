using ConfigurationBinder.Exceptions;
using ConfigurationBinder.Parsers;
using NUnit.Framework;
using System;

namespace ConfigurationBinder.Tests.Parsers
{
    [TestFixture]
    public class DateTimeParserTests
    {
        [Test]
        public void ParseYearMonthDateHourMinuteSecond()
        {
            var parser = new DateTimeParser();
            var input = "2019-10-22 09:10:11";
            var expected = new DateTime(2019, 10, 22, 9, 10, 11);

            Assert.AreEqual(parser.Parse(input), expected);
        }

        [Test]
        public void ThrowOnInvalidDateTime()
        {
            var parser = new DateTimeParser();
            var input = "2019-13-22 09:10:11";
            
            var ex = Assert.Throws<ParsingException>(() => parser.Parse(input));
            
            Assert.That(ex.Input == input);
            Assert.That(ex.TargetType == typeof(DateTime));
        }
    }
}