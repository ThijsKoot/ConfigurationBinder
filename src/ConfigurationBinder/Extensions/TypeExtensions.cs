using System;
using System.Collections;
using System.Linq;

namespace ConfigurationBinder.Extensions
{
    public static class TypeExtensions 
    {
         public static bool IsEnumerable(this Type type) => type
            .GetInterfaces()
            .Any(x => x == typeof(IEnumerable));
    }
}