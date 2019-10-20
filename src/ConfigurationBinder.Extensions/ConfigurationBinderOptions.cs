namespace ConfigurationBinder.Extensions
{
    public class ConfigurationBinderOptions
    {
        public char ArraySeparator { get; set; }
        public static ConfigurationBinderOptions Default => new ConfigurationBinderOptions() { };
    }
}