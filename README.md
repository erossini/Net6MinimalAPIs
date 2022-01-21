# .Net6 Minimal APIs
From now on, we can create minimal [APIs](https://www.puresourcecode.com/category/dotnet/webapi/) in [NET6](https://www.puresourcecode.com/category/dotnet/net6/) that allows us to write in few lines of code powerful APIs. In this post, I collect all my understanding about this new powerful feature.

This code is related to my post [Minimal APIs in NET6](https://www.puresourcecode.com/dotnet/net6/minimal-apis-in-net6/) on [PureSourceCode](https://www.puresourcecode.com).

## The code
The repository contains:
- **src**: source code of the minimal APIs with Swagger using Entity Framework Core for the database in memory and AutoMapper
- **Tests**: test APIs with NUnit
- **xTests**: test APIs with xUnit

## Basic concepts
So, the core idea behind minimal APIs is to remove some of the ceremony of creating simple APIs. It means defining lambda expressions for individual API calls. For example, this is as simple as it gets:

```
app.MapGet("/", () => "Hello World!");
```

This code specifies a route (e.g., “/”) and a callback to execute once a request that matches the route and verb are matched. The method MapGet is specifically to map a HTTP GET to the callback function. So, much of the magic is in the type inference that’s happening. When we return a string (like in this example), it’s wrapping that in a HTTP code 200 (e.g., OK) return result.

How do you even call this? Effectively, these mapping methods are exposed. They’re extension methods on the IEndpointRouteBuilder interface. This interface is exposed by the WebApplication class that’s used to create a new Web server application in .NET 6.
