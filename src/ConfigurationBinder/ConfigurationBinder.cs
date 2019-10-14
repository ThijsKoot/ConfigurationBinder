using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationBinder
{
    public static class Extensions
    {
        /// <summary>
        /// Finds keys in the format "ObjectTypeName.PropertyName"
        /// and binds them to the corresponding properties on the target type
        /// </summary>
        /// <param name="TConfiguration">Target type to bind settings to</param>
        /// <remarks></remarks>
        public static void AddConfiguration<TConfig>(this IServiceCollection services) 
            where TConfig : class, new()
        {
            services.AddOptions<TConfig>()
                .Configure<IConfiguration>((settings, configuration) =>
                    BindSettings(configuration, settings));
        }

        public static void BindSettings(IConfiguration configuration, object target)
        {
            var type = target.GetType();

            foreach (var prop in type.GetProperties())
            {
                var dictKey = $"{type.Name}.{prop.Name}";
                object value;

                if (prop.PropertyType == typeof(Uri))
                    value = new Uri(configuration[dictKey]);
                else
                    value = configuration[dictKey];

                value = Convert.ChangeType(value, prop.PropertyType);
                prop.SetValue(target, value);
            }
        }
    }
}