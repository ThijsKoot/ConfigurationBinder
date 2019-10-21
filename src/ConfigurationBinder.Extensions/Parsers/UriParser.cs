using System;
using ConfigurationBinder.Extensions.Exceptions;

namespace ConfigurationBinder.Extensions.Parsers
{
    public class UriParser : IParser
    {
        public object Parse(string value)
        {   
            try
            {
                return new Uri(value);
            }
            catch (UriFormatException)
            {
                throw new ParsingException(value, typeof(Uri));
            }
        }
    }
}