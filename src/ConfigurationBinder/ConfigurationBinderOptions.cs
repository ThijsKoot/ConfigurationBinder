using System;

namespace ConfigurationBinder
{
    public class ConfigurationBinderOptions
    {
        public char ArraySeparator { get; set; } = ',';
        public StringComparison KeyComparison { get; set; } = StringComparison.CurrentCultureIgnoreCase;
        public static ConfigurationBinderOptions Default => new ConfigurationBinderOptions() { };
    }
}