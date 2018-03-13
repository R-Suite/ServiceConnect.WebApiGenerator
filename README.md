# ServiceConnect.WebApiGenerator

WebApiGenerator is a ServiceConnect extension that generates Swagger UI exposing every handler as an Action Method in ASP.NET Core MVC controller.

## Usage

To use WebApiGenerator simply install the latest version of ServiceConnect.WebApiGenerator from Nuget.org and invoke the Bus extension method - RunWebApiHost()


```c#
static void Main(string[] args)
{
    var bus = Bus.Initialize();
    
    bus.RunWebApiHost();
}
```

You can then access the Swagger UI at http://localhost:5000/swagger/  

Custom url can be passed as a parameter to RunWebApiHost().
```c#
bus.RunWebApiHost("http://localhost:5050");
```
