using JardiiApp.Models;
using JardiiApp.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase("Jardii"));

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

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

app.MapGet("/orders", async (OrderContext context) => {
    return await context.Orders.Include(x => x.Items).ToListAsync();
});

app.MapGet("/order/{id}", async (OrderContext context, int id) =>    
{
    Order? record = await context.Orders.Include(x => x.Items)
        .Where(x => x.Number.Equals(id)).FirstOrDefaultAsync();
    return record is Order order
    ? Results.Ok(order) 
    : Results.NotFound();
});

app.MapPost("/order", async (OrderContext context, Order order) =>
{
    context.Orders.Add(order);
    await context.SaveChangesAsync();

    return Results.Created($"/order/{order.Id}", order);
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
