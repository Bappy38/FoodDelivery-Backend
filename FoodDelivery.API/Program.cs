using FoodDelivery.API.Constants;
using FoodDelivery.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .RegisterServices()
    .ConfigureCorsPolicy(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Cors.FoodDeliveryClientCors);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();