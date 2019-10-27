using System;
using ConfigurationBinder.Exceptions;

namespace ConfigurationBinder.Parsers
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