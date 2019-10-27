using System.Linq;
using ConfigurationBinder.Parsers;
using Microsoft.Extensions.Configuration;

namespace ConfigurationBinder.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void BindSettings(this IConfiguration configuration, object target, ConfigurationBinderOptions options)
        {
            var type = target.GetType();
            var settableProperties = type
                .GetProperties()
                .Where(prop => prop.GetSetMethod() != null);

            foreach (var prop in settableProperties)
            {
                var key = $"{type.Name}.{prop.Name}";

                string value = configuration
                    .AsEnumerable()
                    .FirstOrDefault(kv => kv.Key.Equals(key, options.KeyComparison))
                    .Value;

                if (value != null)
                {
                    object propertyValue = ParserFactory
                                        .CreateParser(prop.PropertyType, options)
                                        .Parse(value);

                    prop.Assign(target, propertyValue);
                }
            }
        }
    }
}