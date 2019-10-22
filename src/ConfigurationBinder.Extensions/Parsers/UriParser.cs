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
                // Future addition for settings: UriKind RelativeOrAbsolute or Absolute
                // RelativeOrAbsolute means that malformed absolute uris don't throw exceptions though
                return new Uri(value, UriKind.Absolute);
            }
            catch (UriFormatException ex)
            {
                throw new ParsingException(value, typeof(Uri), ex);
            }
        }
    }
}