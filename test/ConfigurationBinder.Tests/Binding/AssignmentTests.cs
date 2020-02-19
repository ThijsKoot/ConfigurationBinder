using System;
using System.Collections.Generic;
using System.Reflection;
using ConfigurationBinder.Extensions;
using NUnit.Framework;

namespace ConfigurationBinder.Tests.Binding
{
    [TestFixture]
    public class AssignmentTests
    {
        [Test]
        public void AssignToIntArrayProperty()
        {
            var targetObj = new ConfigurationObject();
            object[] value = { 1, 2, 3 };

            var propInfo = targetObj.GetType().GetProperty(nameof(targetObj.ArrayOfInts));
            int[] expected = { 1, 2, 3 };
            Assert.DoesNotThrow(() => propInfo.Assign(targetObj, value));

            Assert.AreEqual(targetObj.ArrayOfInts, expected);
        }

        [Test]
        public void AssignToUriEnumerable()
        {
            var targetObj = new ConfigurationObject();
            var arrayType = typeof(int);
            object[] value = { new Uri("https://google.com"), new Uri("https://microsoft.com") };
            var propInfo = targetObj.GetType().GetProperty(nameof(targetObj.IEnumerableOfUris));

            Uri[] expected = { new Uri("https://google.com"), new Uri("https://microsoft.com") };

            Assert.DoesNotThrow(() => propInfo.Assign(targetObj, value));

            Assert.AreEqual(targetObj.IEnumerableOfUris, expected);
        }

        [Test]
        public void ThrowExceptionOnNonAssignable()
        {
            int[] value = { 1, 2, 3 };
            var targetObj = new ConfigurationObject();
            var propInfo = targetObj.GetType().GetProperty(nameof(targetObj.ListOfInts));

            Assert.Throws<ArgumentException>(() => propInfo.Assign(targetObj, value));
        }
    }
}