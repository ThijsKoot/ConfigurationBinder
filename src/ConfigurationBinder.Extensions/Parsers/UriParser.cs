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
                return new Uri(value, UriKind.RelativeOrAbsolute);
            }
            catch (UriFormatException ex)
            {
                throw new ParsingException(value, typeof(Uri), ex);
            }
        }
    }
}