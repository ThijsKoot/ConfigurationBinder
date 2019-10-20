# ConfigurationBinder
Extensions methods to deal with the lack of nested objects in Azure Functions v2 Settings. This extension allows you to bind settings to configuration-classes during service registration. 

## Usage

```csharp
class RandomConfiguration
{
    public string Host { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
}
```

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

This will add `IOptions<TConfig>` to the service collection.
```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddConfiguration<TConfig>();
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