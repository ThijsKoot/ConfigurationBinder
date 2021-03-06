using System;
using ConfigurationBinder.Exceptions;

namespace ConfigurationBinder.Parsers
{
    public class EnumParser : IParser
    {
        private readonly Type _targetType;

        public EnumParser(Type targetType)
        {
            this._targetType = targetType;
        }

        public object Parse(string value)
        {
            if(Enum.TryParse(_targetType, value, true, out object result))
                return result;
            
            throw new ParsingException(value, _targetType);
        }
    }
}