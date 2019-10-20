namespace ConfigurationBinder
{
    public class ConfigurationBinderOptions
    {
        public char ArraySeparator { get; set; }
        public static ConfigurationBinderOptions Default => new ConfigurationBinderOptions() { };
    }
}