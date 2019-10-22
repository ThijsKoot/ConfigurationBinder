using System;

namespace ConfigurationBinder.Tests.Binding
{
    public class ConfigurationObject
    {
        public string String { get; set; }
        public int Int { get; set; }
        public Uri Uri { get; set; }
        public Guid Guid { get; set; }
        public int[] ArrayOfInts {get;set;}
        
        
    }
}