using FoodDelivery.API.Constants;
using Serilog.Core;
using Serilog.Events;

namespace FoodDelivery.API.LogEnrichers;

public class CorrelationIdEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var correlationId = "DummyCorrelationId";
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("CorrelationId", correlationId));
    }
}