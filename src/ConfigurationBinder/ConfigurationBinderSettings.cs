namespace ConfigurationBinder
{
    public class ConfigurationBinderOptions
    {
        public static char ArraySeparator { get; set; }
        public static ConfigurationBinderOptions Default => new ConfigurationBinderOptions() { };
    }
}