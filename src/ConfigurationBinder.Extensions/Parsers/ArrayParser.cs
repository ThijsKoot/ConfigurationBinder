using System;
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
            _separator = separator;
            _targetType = targetType;
        }

        public object Parse(string value)
        {
            var parser = ParserFactory.CreateParser(_targetType);
            
            try
            {
                return value
                    .Split(_separator)
                    .Select(x => parser.Parse(x))
                    .ToArray();
            }
            catch (ParsingException ex)
            {
                throw new ParsingException(value, _targetType.MakeArrayType(), ex);
            }
        }
    }
}