using Microsoft.OpenApi.Models;
using seller_orderservice_api.Application.DTOs;
using seller_orderservice_api.Domain.Entities;
using seller_orderservice_api.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IOrderService, OrderService>();

// Adiciona serviços ao contêiner (como Controllers)
builder.Services.AddControllers();

// Configura o Swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Seller Order Service API", // Nome da API
        Version = "v1",                    // Versão da API
        Description = "API para gerenciar pedidos de venda" // Descrição da API
    });
});

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    // Habilita o Swagger e o SwaggerUI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Seller Order Service API v1");
        c.RoutePrefix = string.Empty; // Acesse o Swagger na raiz (http://localhost:5000)
    });
}

app.UseHttpsRedirection();

// Exemplo de endpoint
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

// Modelo de previsão do tempo
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
