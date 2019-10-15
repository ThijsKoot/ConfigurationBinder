using System;
using System.Collections.Generic;
using ConfigurationBinder.Parsers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationBinder
{
    public static class Extensions
    {
        public static ConfigurationBinderOptions Options { get; private set; }

        /// <summary>
        /// Finds keys in the format "ObjectTypeName.PropertyName"
        /// and binds them to the corresponding properties on the target type
        /// </summary>
        /// <param name="TConfiguration">Target type to bind settings to</param>
        /// <remarks></remarks>
        public static void AddConfiguration<TConfig>(this IServiceCollection services)
            where TConfig : class, new() =>
                services.AddConfiguration<TConfig>(ConfigurationBinderOptions.Default);

        public static void AddConfiguration<TConfig>(this IServiceCollection services, ConfigurationBinderOptions options)
            where TConfig : class, new()
        {
            services.AddOptions<TConfig>()
                .Configure<IConfiguration>((target, configuration) =>
                    BindSettings(configuration, target, options));
        }

        public static void BindSettings(IConfiguration configuration, object target, ConfigurationBinderOptions options)
        {
            var type = target.GetType();

            foreach (var prop in type.GetProperties())
            {
                var key = $"{type.Name}.{prop.Name}";
                string value = configuration[key];
                object propertyValue;

                var parser = new Parser(prop.PropertyType);
                propertyValue = parser.Parse(value);

                prop.SetValue(target, propertyValue);
            }
        }
    }
}