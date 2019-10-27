using System;
using System.Collections.Generic;

namespace ConfigurationBinder.Tests.Binding
{
    public class ConfigurationObject
    {
        public string String { get; set; }
        public int Int { get; set; }
        public Uri Uri { get; set; }
        public Guid Guid { get; set; }
        public int[] ArrayOfInts { get; set; }
        public IEnumerable<Uri> IEnumerableOfUris { get; set; }
        public List<int> ListOfInts { get; set; }
        public CustomValuesEnum CustomValuesEnum { get; set; }
        public DefaultValuesEnum DefaultValuesEnum { get; set; }
        public DateTime DateTime { get; set; }
        public int NonSettableInt => 1;
        public int PrivateSettableInt { get; private set; }
        private int _privateInt { get; set; }
    }

    public enum DefaultValuesEnum
    {
        Foo,
        Bar,
        Baz
    }

    public enum CustomValuesEnum
    {
        Foo = 0,
        Bar = 5,
        Baz = 10
    }
}