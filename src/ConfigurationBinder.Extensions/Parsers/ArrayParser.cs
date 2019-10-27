using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConfigurationBinder.Extensions.Exceptions;

namespace ConfigurationBinder.Extensions.Parsers
{
    public class ArrayParser : IParser
    {
        private readonly char _separator;
        private readonly Type _targetType;

        public ArrayParser(char separator, Type targetType)
        {
            // IDictionary should definitely fail. Not sure if only allowing IEnumerable<>
            // and filtering out Dictionary<,> is enough
            if (targetType.GetInterfaces().Any(x => x == typeof(IDictionary<,>)))
                throw new ArgumentException("Parsing IDictionary<,> is not supported");

            _separator = separator;
            _targetType = targetType;
        }

        public Type ElementType =>  _targetType.IsArray 
                ? _targetType.GetElementType() 
                : _targetType.GetGenericArguments()[0];

        public Type TargetType => _targetType;

        public object Parse(string value)
        {                
            var parser = ParserFactory.CreateParser(ElementType);

            try
            {
                return value
                    .Split(_separator)
                    .Select(x => parser.Parse(x))
                    .ToArray();
            }
            catch (ParsingException ex)
            {
                throw new ParsingException(value, _targetType, ex);
            }
        }
    }
}