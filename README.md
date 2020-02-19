# ConfigurationBinder
![AppVeyor](https://img.shields.io/appveyor/ci/ThijsKoot/configurationbinder?label=master)
![Nuget](https://img.shields.io/nuget/dt/configurationbinder)
![AppVeyor branch](https://img.shields.io/appveyor/ci/thijskoot/configurationbinder/dev?label=dev)

Extensions methods to deal with the lack of nested objects in Azure Functions v2 Settings. This extension allows you to bind settings to configuration-classes during service registration. 

## Usage

This will add `IOptions<TConfig>` to the service collection.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddConfiguration<TConfig>();
}
```

Example TConfig:
```csharp
class RandomConfiguration
{
    public string Host { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
}
```

Example appsettings.json
```json
{
    "Values": {
        "Value1":"",
        "RandomConfiguration.User": "user1",
        "RandomConfiguration.Host": "host.domain",
        "RandomConfiguration.Password": "secret"
        }
}
```


## Supported property types

All basic types are supported. Non-settable or privately settable properties will obviously not be set but can be present on TConfig without triggering exceptions.

Parsing is supported for:
* All basic (value) types, i.e. `string`, `int`, `decimal`, `float`, etc
* `DateTime`
* `Uri`
* `Guid`
* `Enum`
* Types that implement `IEnumerable<T>` and are assignable from an `Array` of `T`. `T` must be a type supported by this project


See sample object with a non-exhaustive list of supported properties below.

```csharp
public class ConfigurationObject
{
        public string String { get; set; }
        public int Int { get; set; }
        public Uri Uri { get; set; }
        public Guid Guid { get; set; }
        public int[] ArrayOfInts { get; set; }
        public IEnumerable<Uri> IEnumerableOfUris { get; set; }
        public CustomValuesEnum CustomValuesEnum { get; set; }
        public DefaultValuesEnum DefaultValuesEnum { get; set; }
        public DateTime DateTime { get; set; }
        public int NonSettableInt => 1;
        public int PrivateSettableInt { get; private set; }
        private int _privateInt { get; set; }
}
```

Matching appsettings.json:
```json
{
    "Values": {
        "configurationObject.string": "The quick brown fox jumps over the lazy dog",
        "configurationObject.int": "1",
        "configurationObject.uri": "https://google.com",
        "configurationObject.guid": "b7164fbe-08a3-4cca-910a-ec360b525ccf",
        "configurationObject.arrayOfInts": "1,2,3,4",
        "configurationObject.ienumerableOfUris": "https://google.com,https://microsoft.com,https://github.com",
        "configurationObject.customValuesEnum": "0",
        "configurationObject.defaultValuesEnum": "Bar",
        "configurationObject.dateTime": "2019-01-02 03:04:05"
    }
}
```

## ConfigurationBinderOptions

Option          | Description                                                       | Default
--------        |-------------                                                      |--------
ArraySeparator  | `char` used to separate items in an array.                        | `,`
StringComparison|Determines how to match keys in appsettings with property names    | StringComparison.CurrentCultureIgnoreCase


Usage of options: 
```csharp
public void ConfigureServices(IServiceCollection services)
{
    var bindingOptions = ConfigurationBinderOptions.Default;
    bindingOptions.ArraySeparator = ';';

    services.AddConfiguration<TConfig>(bindingOptions);
}
```

## The problem
You'd expect to be able to bind the following Configuration class to a corresponding section in appsettings.json, but it you can't. This is because for Functions v2 appsettings are treated as `Dictionary<string, string>`.


This doesn't work. It won't report errors though.
```json
{
    "Values": {
        "Value1":""
    },
    "RandomConfiguration": {
        "User": "user1",
        "Host": "host.domain",
        "Password": "secret"
        }
}
```

This doesn't work either, and it'll report an exception.
```json
{
    "Values": {
        "Value1":"",
        "RandomConfiguration": {
            "User": "user1",
            "Host": "host.domain",
            "Password": "secret"
        }
}
```

See: 
* [Github comment](https://github.com/Azure/azure-functions-core-tools/issues/1473#issuecomment-325045802)