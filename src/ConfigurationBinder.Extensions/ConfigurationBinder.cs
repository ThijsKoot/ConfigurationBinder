using System;
using System.Collections.Generic;
using ConfigurationBinder.Extensions.Parsers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            foreach (var prop in type.GetProperties())
            {
                var key = $"{type.Name}.{prop.Name}";
                string value = configuration[key];
                object propertyValue;

                var parser = new Parser(prop.PropertyType, options);
                propertyValue = parser.Parse(value);

                prop.SetValue(target, propertyValue);
            }
        }
    }
}