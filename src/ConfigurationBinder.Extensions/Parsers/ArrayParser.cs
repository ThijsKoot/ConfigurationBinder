using System;
using System.Linq;
using System.Reflection;

namespace ConfigurationBinder.Extensions.Parsers
{
    public class ArrayParser : IParser
    {
        private readonly char _separator;
        private readonly Type _targetType;

        public ArrayParser(char separator, Type targetType)
        {
            if(targetType == null) throw new ArgumentNullException(nameof(targetType));

            if(targetType.BaseType != typeof(ValueType))
                throw new ArgumentException($"ArrayParser cannot be instantiated for type {targetType}");
            
            _separator = separator;
            _targetType = targetType;
        }

        public object Parse(string value)
        {
            var splitValues = value.Split(_separator);

            return splitValues.Select(x => Convert.ChangeType(x, _targetType));
        }
    }
}