using System;
using UriParser = ConfigurationBinder.Extensions.Parsers.UriParser;

namespace ConfigurationBinder.Extensions.Parsers
{
    public class Parser : IParser
    {
        private readonly IParser _internalParser;
        public Parser(Type type, ConfigurationBinderOptions options)
        {
            _internalParser = ParserFactory.GetParser(type, options);
        }

        public object Parse(string value) => _internalParser.Parse(value);
    }
}