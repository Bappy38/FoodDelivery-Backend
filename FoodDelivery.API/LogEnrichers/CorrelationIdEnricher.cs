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