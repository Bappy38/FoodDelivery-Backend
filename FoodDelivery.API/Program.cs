using FoodDelivery.API.Constants;
using FoodDelivery.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterServices();

builder.Services.AddControllers();

builder.Services.AddCors(cors =>
{
    cors.AddPolicy(Cors.FoodDeliveryClientCors, policy =>
    {
        policy
        .WithOrigins("http://localhost:1234")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

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