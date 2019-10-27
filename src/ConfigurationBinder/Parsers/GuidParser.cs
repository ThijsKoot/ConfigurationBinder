using System;
using ConfigurationBinder.Exceptions;

namespace ConfigurationBinder.Parsers
{
    public class GuidParser : IParser
    {
        public object Parse(string value)
        {
            if (!Guid.TryParse(value, out Guid result))
                throw new ParsingException(value, typeof(Guid));
                
            return result;
        }
    }
}