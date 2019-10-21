using System;
using UriParser = ConfigurationBinder.Extensions.Parsers.UriParser;

namespace ConfigurationBinder.Extensions.Parsers
{
    public class Parser : IParser
    {
        private readonly IParser _internalParser;
        private readonly ConfigurationBinderOptions _options;
        public Parser(Type type, ConfigurationBinderOptions options)
        {
            _options = options;

            if(type == typeof(Guid))
                _internalParser = new GuidParser();
            else if (type == typeof(Uri))
                _internalParser = new UriParser();
            else if (type == typeof(Array))
                _internalParser = new ArrayParser(_options.ArraySeparator, type);
            else
                _internalParser = new DefaultParser(type);
        }

        public object Parse(string value) => _internalParser.Parse(value);
    }
}