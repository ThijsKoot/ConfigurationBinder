namespace ConfigurationBinder.Parsers
{
    internal class ArrayParser : IParser
    {
        private readonly char _arraySeparator;
        public ArrayParser(char arraySeparator)
        {
            _arraySeparator = arraySeparator;
        }
        
        public object Parse(object value)
        {
            throw new System.NotImplementedException();
        }
    }
}