using System;

namespace ConfigurationBinder.Extensions.Parsers
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
            return Convert.ChangeType(value, _targetType);
        }
    }
}