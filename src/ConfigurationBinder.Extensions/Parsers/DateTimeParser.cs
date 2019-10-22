using System;
using ConfigurationBinder.Extensions.Exceptions;

namespace ConfigurationBinder.Extensions.Parsers
{
    public class DateTimeParser : IParser
    {
        public object Parse(string value)
        {
            if(DateTime.TryParse(value, out DateTime result))
                return result;
            
            throw new ParsingException(value, typeof(DateTime));
        }
    }
}