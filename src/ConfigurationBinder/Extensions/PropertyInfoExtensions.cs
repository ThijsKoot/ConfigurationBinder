using System;
using System.Reflection;

namespace ConfigurationBinder.Extensions
{
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Sets the value of a property on a given object.
        /// </summary>
        /// <param name="prop">PropertyInfo to set</param>
        /// <param name="target">Target to set property value on</param>
        /// <param name="value">Value to set</param>
        /// <remarks>Regular SetValue-assignment doesn't work for arrays of value types</remarks>
        public static void Assign(this PropertyInfo prop, object target, object value)
        {
            var propertyType = prop.PropertyType;

            // Check if array assignment is needed. It needs a bit more voodoo
            // to cope with assigning an array of object to an array of valuetypes
            if (propertyType.IsEnumerable()
                && propertyType != typeof(string)
                && value is Array array)
            {
                var elementType = propertyType.IsGenericType 
                    ? propertyType.GetGenericArguments()[0] 
                    : propertyType.GetElementType();

                var arrayType = elementType.MakeArrayType();

                var creationArgs = new object[] { array.Length };

                var targetArray = (Array)Activator.CreateInstance(arrayType, creationArgs);

                for (int i = 0; i < array.Length; i++)
                {
                    var elementValue = Convert.ChangeType(array.GetValue(i), elementType);
                    targetArray.SetValue(elementValue, i);
                }

                if(!propertyType.IsAssignableFrom(arrayType))
                    throw new ArgumentException($"Type ${propertyType} is not assignable from array of ${arrayType}");

                prop.SetValue(target, targetArray);
            }
            else
            {
                // Just convert and set the value
                var targetValue = Convert.ChangeType(value, propertyType);
                prop.SetValue(target, targetValue);
            }
        }
    }
}