using System;
using ConfigurationBinder.Exceptions;

namespace ConfigurationBinder.Parsers
{
    public class DefaultParser : IParser
    {
        private readonly Type _targetType;
        public DefaultParser(Type targetType)
        {
            _targetType = targetType;
        }

        public object Parse(string value)
        {
            try
            {
                return Convert.ChangeType(value, _targetType);
            }
            catch (FormatException ex)
            {
                throw new ParsingException(value, _targetType, ex);
            }
        }
    }
}