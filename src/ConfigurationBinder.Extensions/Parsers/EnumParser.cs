using System;

namespace ConfigurationBinder.Extensions.Parsers
{
    public class EnumParser : IParser
    {
        private readonly Type type;

        public EnumParser(Type type)
        {
            this.type = type;
        }

        public object Parse(string value)
        {
            throw new NotImplementedException();
        }
    }
}