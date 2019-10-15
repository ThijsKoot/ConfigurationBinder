using System;
using UriParser = ConfigurationBinder.Parsers.UriParser;

namespace ConfigurationBinder.Parsers
{
    public class Parser : IParser
    {
        private readonly IParser _internalParser;

        public Parser(Type type)
        {
            if(type == typeof(Guid))
                _internalParser = new GuidParser();
            else if (type == typeof(Uri))
                _internalParser = new UriParser();
            else if (type == typeof(Array))
                _internalParser = new ArrayParser();
            else
                _internalParser = new DefaultParser();
        }

        public object Parse(object value)
        {
            throw new System.NotImplementedException();
        }
    }
}