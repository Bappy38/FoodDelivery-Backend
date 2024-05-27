using FoodDelivery.API.Constants;
using FoodDelivery.API.Extensions;
using FoodDelivery.API.LogEnrichers;
using FoodDelivery.API.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .RegisterServices()
    .ConfigureCorsPolicy(builder.Configuration);

builder.Services.AddControllers();

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration((context.Configuration));
    config.Enrich.With<CorrelationIdEnricher>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Cors.FoodDeliveryClientCors);

app.UseHttpsRedirection();

app.UseMiddleware<CorrelationIdCreatorMiddleware>();

app.MapControllers();

app.UseSerilogRequestLogging();

app.Run();