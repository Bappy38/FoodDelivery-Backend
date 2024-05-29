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