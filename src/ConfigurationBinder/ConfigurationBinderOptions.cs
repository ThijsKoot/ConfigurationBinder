using System;

namespace ConfigurationBinder
{
    public class ConfigurationBinderOptions
    {
        /// <summary>
        /// Character separating array values
        /// </summary>
        /// <value></value>
        public char ArraySeparator { get; set; } = ',';

        /// <summary>
        /// Separates class- and propertynames in the configuration keys
        /// </summary>
        /// <value></value>
        public string KeySeparator { get; set; } = ".";

        /// <summary>
        /// StringComparison strategy for matching keys to class properties
        /// </summary>
        /// <value></value>
        public StringComparison KeyComparison { get; set; } = StringComparison.CurrentCultureIgnoreCase;

        public static ConfigurationBinderOptions Default => new ConfigurationBinderOptions();
    }
}