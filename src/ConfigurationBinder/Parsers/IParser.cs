namespace ConfigurationBinder.Parsers
{
    public interface IParser
    {
        object Parse(string value);
    }
}