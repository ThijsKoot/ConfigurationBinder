using System;

namespace ConfigurationBinder.Extensions.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException(string input, Type targetType)
            : base($"Could not parse input {input} to type {targetType.Name}")
        { 
            Input = input;
            TargetType = targetType;
        }
        
        public string Input {get;set;}
        public Type TargetType { get; set; }
    }
}