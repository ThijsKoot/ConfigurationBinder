using System;
using System.Collections.Generic;
using ConfigurationBinder.Extensions.Parsers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using ConfigurationBinder.Extensions.Extensions;

namespace ConfigurationBinder.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static ConfigurationBinderOptions Options { get; private set; }

        /// <summary>
        ///  Finds keys in the format "ObjectTypeName.PropertyName"
        ///  and binds them to the corresponding properties on the target type
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TConfig">Target type to bind settings to</typeparam>
        public static void AddConfiguration<TConfig>(this IServiceCollection services)
            where TConfig : class, new() =>
                services.AddConfiguration<TConfig>(ConfigurationBinderOptions.Default);

        /// <summary>
        /// Finds keys in the format "ObjectTypeName.PropertyName"
        /// and binds them to the corresponding properties on the target type
        /// </summary>
        /// <param name="services">Service Collection</param>
        /// <param name="options">Configuration Binder options</param>
        /// <typeparam name="TConfig">Target type to bind settings to</typeparam>
        public static void AddConfiguration<TConfig>(this IServiceCollection services, ConfigurationBinderOptions options)
            where TConfig : class, new() =>
            services.AddOptions<TConfig>()
                .Configure<IConfiguration>((target, configuration) =>
                    BindSettings(configuration, target, options));

        public static void BindSettings(IConfiguration configuration, object target, ConfigurationBinderOptions options)
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

                object propertyValue = ParserFactory
                    .CreateParser(prop.PropertyType, options)
                    .Parse(value);

                AssignProperty(prop, target, propertyValue);
            }
        }

        public static void AssignProperty(PropertyInfo prop, object target, object value)
        {
            var propertyType = prop.PropertyType;

            if (propertyType.IsEnumerable()
                && propertyType != typeof(string)
                && value is Array array)
            {

                var elementType = propertyType.IsGenericType 
                    ? propertyType.GetGenericArguments()[0] 
                    : propertyType.GetElementType();

                var arrayType = elementType.MakeArrayType();

                var creationArgs = new object[] { array.Length };

                var targetArray = (Array)Activator.CreateInstance(arrayType, creationArgs);

                for (int i = 0; i < array.Length; i++)
                {
                    var elementValue = Convert.ChangeType(array.GetValue(i), elementType);
                    targetArray.SetValue(elementValue, i);
                }

                prop.SetValue(target, targetArray);
            }
            else
            {
                prop.SetValue(target, value);
            }
        }
    }
}