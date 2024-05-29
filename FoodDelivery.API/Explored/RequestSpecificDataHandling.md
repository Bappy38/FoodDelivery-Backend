# How to handle request specific data in .NET Core?

**Scenario:** Some times we need to store request specific data such as user claims, requestId / correlationId so that we can access these data all over the application. How can we handle such scenario?

## Solution-1: `HttpContext.Items`

### Step-1: Create a service to Store and Retrieve Data

```

namespace FoodDelivery.API.Services;

public interface IHttpContextAccessorService
{
    void SetItem(string key, object value);
    object? GetItem(string key);
}

public class HttpContextAccessorService : IHttpContextAccessorService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextAccessorService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetItem(string key, object value)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null)
        {
            return;
        }

        httpContext.Items[key] = value;
    }

    public object? GetItem(string key)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null || !httpContext.Items.ContainsKey(key))
        {
            return null;
        }

        return httpContext.Items[key];
    }
}

```

### Step-2: Register the Service in DI Container

Register the `HttpContextAccessor` provided by .NET Core and the Custom Service `HttpContextAccessorService` we wrote to abstract away the get and set logic.

**We must have to registered the service with scoped lifetime**

```

public static IServiceCollection RegisterServices(this IServiceCollection services)
{
    //Other services

    services.AddHttpContextAccessor();
    services.AddScoped<IHttpContextAccessorService, HttpContextAccessorService>();

    return services;
}

```

### Step-3: Use the Service Anywhere you need

```

using FoodDelivery.API.Constants;
using FoodDelivery.API.DTOs;
using FoodDelivery.API.Models;
using FoodDelivery.API.Queries;
using FoodDelivery.API.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FoodDelivery.API.Repositories;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllAsync();
    List<Restaurant> FilterRestaurant(RestaurantFilterDto filter);
    RestaurantMenu GetMenuByRestaurantId(int restaurantId);
    RestaurantDetailDto GetRestaurantDetail(int restaurantId);
}

public class RestaurantRepository : IRestaurantRepository
{
    private ILogger<RestaurantRepository> _logger;
    private readonly IHttpContextAccessorService _contextAccessorService;

    public RestaurantRepository(ILogger<RestaurantRepository> logger, IHttpContextAccessorService contextAccessorService)
    {
        _logger = logger;
        _contextAccessorService = contextAccessorService;
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        _logger.LogInformation(_contextAccessorService.GetItem("CorrelationId").ToString());

        //Other business logic

        return restaurants;
    }
}


```


## Solution-2: `AsyncLocal<T>`

### Step-1: Create a class which contains a AsyncLocal variable. We will store the request specific data into that variable

**AsyncLocal must have to static or singleton**

```

namespace FoodDelivery.API.Services;

public static class RequestContext
{
    private static AsyncLocal<string?> _correlationId = new AsyncLocal<string?>();

    public static string? CorrelationId
    {
        get => _correlationId.Value;
        set => _correlationId.Value = value;
    }
}

```

### Step-2: Just access that static variable wherever you need

```

using FoodDelivery.API.Constants;
using FoodDelivery.API.Services;

namespace FoodDelivery.API.Middlewares;

public class CorrelationIdCreatorMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdCreatorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey(RequestHeaders.CorrelationId))
        {
            context.Request.Headers.TryAdd(RequestHeaders.CorrelationId, Guid.NewGuid().ToString());
        }

        var correlationId = context.Request.Headers[RequestHeaders.CorrelationId];
        RequestContext.CorrelationId = correlationId;

        context.Response.OnStarting(() =>
        {
            context.Response.Headers.TryAdd(RequestHeaders.CorrelationId, context.Request.Headers[RequestHeaders.CorrelationId]);
            return Task.CompletedTask;
        });

        await _next(context);
    }
}

```


```

using FoodDelivery.API.Constants;
using FoodDelivery.API.Services;
using Serilog.Core;
using Serilog.Events;

namespace FoodDelivery.API.LogEnrichers;

public class CorrelationIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationId", RequestContext.CorrelationId));
    }
}

```

## Pros of using `HttpContext.Items`

- 



## Pros of using `AsyncLocal`

- Whenever we update the value, only the subsequent thread will get the updated value.
- As it's a static variable, accessing from anywhere is much easier.