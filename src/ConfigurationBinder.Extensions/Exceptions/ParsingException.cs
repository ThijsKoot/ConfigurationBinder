using System;

namespace ConfigurationBinder.Extensions.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException(string input, Type targetType) : this(input, targetType, null) { }
        
        public ParsingException(string input, Type targetType, Exception innerException) 
            : base(GetMessage(input, targetType), innerException)
        { 
            Input = input;
            TargetType = targetType;
        }

        private static string GetMessage(string input, Type targetType) => 
            $"Could not parse input {input} to type {targetType.Name}";

        public string Input {get;set;}
        public Type TargetType { get; set; }
    }
}