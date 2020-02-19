using System;
using System.Collections.Generic;
using ConfigurationBinder.Extensions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using static ConfigurationBinder.Extensions.ServiceCollectionExtensions;

namespace ConfigurationBinder.Tests.Binding
{
    [TestFixture]
    public class BindingTests
    {
        private IConfiguration _configuration;
        private ConfigurationObject _expected;

        [SetUp]
        public void Init()
        {
            _expected = new ConfigurationObject
            {
                String = "The quick brown fox jumps over the lazy dog",
                Int = 1,
                Uri = new Uri("https://google.com"),
                Guid = Guid.Parse("b7164fbe-08a3-4cca-910a-ec360b525ccf"),
                ArrayOfInts = new int[] { 1, 2, 3, 4 },
                IEnumerableOfUris = new Uri[] { new Uri("https://google.com"), new Uri("https://microsoft.com"), new Uri("https://github.com") },
                CustomValuesEnum = CustomValuesEnum.Foo,
                DefaultValuesEnum = DefaultValuesEnum.Bar,
                DateTime = new DateTime(2019, 1, 2, 3, 4, 5)
            };

            var data = new Dictionary<string, string>
            {
                {"configurationObject.string", "The quick brown fox jumps over the lazy dog"},
                {"configurationObject.int", "1"},
                {"configurationObject.uri", "https://google.com"},
                {"configurationObject.guid", "b7164fbe-08a3-4cca-910a-ec360b525ccf"},
                {"configurationObject.arrayOfInts", "1,2,3,4"},
                {"configurationObject.ienumerableOfUris", "https://google.com,https://microsoft.com,https://github.com"},
                {"configurationObject.customValuesEnum", "0"},
                {"configurationObject.defaultValuesEnum", "Bar"},
                {"configurationObject.dateTime" , "2019-01-02 03:04:05"}
            };


            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(data)
                .Build();
        }

        [Test]
        public void BindWithCustomKeySeparator()
        {
            var target = new ConfigurationObject();
            var customOptions = new ConfigurationBinderOptions { KeySeparator = "_" };

            var data = new Dictionary<string, string>
            {
                {"configurationObject_string", "abc"} 
            };

            var expectedValue = "abc";

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(data)
                .Build();

            Assert.DoesNotThrow(() => configuration.BindSettings(target, customOptions));

            Assert.AreEqual(expectedValue, target.String);
        }

        [Test]
        public void BindToObject()
        {
            var target = new ConfigurationObject();
            var options = ConfigurationBinderOptions.Default;

            Assert.DoesNotThrow(() => _configuration.BindSettings(target, options));

            Assert.AreEqual(_expected.ArrayOfInts, target.ArrayOfInts);
            Assert.AreEqual(_expected.CustomValuesEnum, target.CustomValuesEnum);
            Assert.AreEqual(_expected.DateTime, target.DateTime);
            Assert.AreEqual(_expected.DefaultValuesEnum, target.DefaultValuesEnum);
            Assert.AreEqual(_expected.Guid, target.Guid);
            Assert.AreEqual(_expected.IEnumerableOfUris, target.IEnumerableOfUris);
            Assert.AreEqual(_expected.Int, target.Int);
            Assert.AreEqual(_expected.NonSettableInt, target.NonSettableInt);
            Assert.AreEqual(_expected.PrivateSettableInt, target.PrivateSettableInt);
            Assert.AreEqual(_expected.Uri, target.Uri);
        }
    }
}