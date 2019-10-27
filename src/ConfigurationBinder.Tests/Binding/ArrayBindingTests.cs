using System;
using System.Reflection;
using ConfigurationBinder.Extensions;
using NUnit.Framework;

namespace ConfigurationBinder.Tests.Binding
{
    [TestFixture]
    public class ArrayBindingTests
    {
        [Test]
        public void AssignToIntArrayProperty()
        {
            var targetObj = new ConfigurationObject();
            var arrayType = typeof(int);
            object[] value = { 1, 2, 3 };
            var propInfo = targetObj.GetType().GetProperty("ArrayOfInts");
            int[] expected = { 1, 2, 3 };
            Assert.DoesNotThrow(() =>
                ServiceCollectionExtensions.AssignProperty(propInfo, targetObj, value));

            Assert.AreEqual(targetObj.ArrayOfInts, expected);
        }

        [Test]
        public void AssignToUriEnumerable()
        {
            var targetObj = new ConfigurationObject();
            var arrayType = typeof(int);
            object[] value = { new Uri("https://google.com"), new Uri("https://microsoft.com") };
            var propInfo = targetObj.GetType().GetProperty("IEnumerableOfUris");

            Uri[] expected = { new Uri("https://google.com"), new Uri("https://microsoft.com") };

            Assert.DoesNotThrow(() =>
                ServiceCollectionExtensions.AssignProperty(propInfo, targetObj, value));

            Assert.AreEqual(targetObj.IEnumerableOfUris, expected);
        }
    }
}