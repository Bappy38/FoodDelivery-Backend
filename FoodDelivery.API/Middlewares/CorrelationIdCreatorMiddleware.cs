using FoodDelivery.API.Constants;

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

        context.Response.OnStarting(() =>
        {
            context.Response.Headers.TryAdd(RequestHeaders.CorrelationId, context.Request.Headers[RequestHeaders.CorrelationId]);
            return Task.CompletedTask;
        });

        await _next(context);
    }
}