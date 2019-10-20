namespace ConfigurationBinder.Extensions
{
    public interface IParser
    {
        object Parse(string value);
    }
}