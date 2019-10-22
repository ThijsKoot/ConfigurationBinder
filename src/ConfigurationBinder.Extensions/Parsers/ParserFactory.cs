using System;

namespace ConfigurationBinder.Extensions.Parsers
{
    public static class ParserFactory
    {
        public static IParser CreateParser(Type targetType, ConfigurationBinderOptions options)
        {
            if(targetType == typeof(Guid))
                return new GuidParser();
            else if (targetType == typeof(Uri))
                return new UriParser();
            else if (targetType == typeof(Array))
                return new ArrayParser(options.ArraySeparator, targetType);
            else if (targetType.BaseType == typeof(Enum))
                return new EnumParser(targetType);
            else if (targetType == typeof(DateTime))
                return new DateTimeParser();
            else
                return new DefaultParser(targetType);
        }

        public static IParser CreateParser(Type targetType) => 
            ParserFactory.CreateParser(targetType, ConfigurationBinderOptions.Default);
        
    }
}