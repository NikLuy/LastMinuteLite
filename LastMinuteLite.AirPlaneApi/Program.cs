// Program.cs (AirPlane API)
using LastMinuteLite.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Ensure the required package is installed
builder.Services.AddOpenApi();


builder.AddServiceDefaults();


var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

//app.Use(async (ctx, next) =>
//{
//    var path = ctx.Request.Path;

//    // allow docs & UI without token
//    if (path.StartsWithSegments("/openapi") ||
//        path.StartsWithSegments("/swagger") ||
//        path.StartsWithSegments("/scalar"))
//    {
//        await next();
//        return;
//    }

//    var expected = builder.Configuration["Auth:ApiToken"];
//    if (!string.IsNullOrWhiteSpace(expected))
//    {
//        if (!ctx.Request.Headers.TryGetValue("X-Api-Token", out var token) || token != expected)
//        {
//            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
//            await ctx.Response.WriteAsync("Missing or invalid X-Api-Token");
//            return;
//        }
//    }

//    await next();
//});

app.UseHttpsRedirection();

// --- DTO + Random Endpoint ---
app.MapGet("/api/deals/random", () =>
{
    var airlines = new[] { "SkySwiss", "AlpineAir", "EuroJet", "PolarFly" };
    var cities = new[] { "ZRH", "GVA", "LHR", "FRA", "CDG", "JFK", "BCN", "MAD" };
    var rnd = Random.Shared;

    var deal = new FlightDealDto(
        Id: Guid.NewGuid(),
        Airline: airlines[rnd.Next(airlines.Length)],
        From: cities[rnd.Next(cities.Length)],
        To: cities[rnd.Next(cities.Length)],
        DepartureUtc: DateTime.UtcNow.AddHours(rnd.Next(6, 120)),
        Price: Math.Round((decimal)rnd.Next(59, 599) + rnd.NextDecimal(), 2),
        SeatsLeft: rnd.Next(1, 12)
    );

    return Results.Ok(deal);
})
.WithName("GetRandomFlightDeal");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

public record FlightDealDto(
    Guid Id,
    string Airline,
    string From,
    string To,
    DateTime DepartureUtc,
    decimal Price,
    int SeatsLeft
);

file static class DecimalExt
{
    public static decimal NextDecimal(this Random r) => (decimal)r.NextDouble();
}
