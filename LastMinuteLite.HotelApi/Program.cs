// Program.cs (Hotel API)
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.Use(async (ctx, next) =>
{
    var expected = builder.Configuration["Auth:ApiToken"];
    if (!string.IsNullOrWhiteSpace(expected))
    {
        if (!ctx.Request.Headers.TryGetValue("X-Api-Token", out var token) || token != expected)
        {
            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await ctx.Response.WriteAsync("Missing or invalid X-Api-Token");
            return;
        }
    }
    await next();
});

app.UseHttpsRedirection();

app.MapGet("/api/deals/random", () =>
{
    var hotels = new[] { "Hotel Aurora", "Grand Vista", "Sea Breeze", "Mountain Inn" };
    var cities = new[] { "Zürich", "Genf", "London", "Frankfurt", "Paris", "New York", "Barcelona", "Madrid" };
    var rnd = Random.Shared;

    var deal = new HotelDealDto(
        Id: Guid.NewGuid(),
        HotelName: hotels[rnd.Next(hotels.Length)],
        City: cities[rnd.Next(cities.Length)],
        Stars: rnd.Next(2, 6),
        CheckInUtc: DateTime.UtcNow.AddDays(rnd.Next(1, 30)),
        Nights: rnd.Next(1, 7),
        PricePerNight: Math.Round((decimal)rnd.Next(49, 299) + rnd.NextDecimal(), 2),
        RoomsLeft: rnd.Next(1, 8)
    );

    return Results.Ok(deal);
})
.WithName("GetRandomHotelDeal");

app.Run();

public record HotelDealDto(
    Guid Id,
    string HotelName,
    string City,
    int Stars,
    DateTime CheckInUtc,
    int Nights,
    decimal PricePerNight,
    int RoomsLeft
);

file static class DecimalExt
{
    public static decimal NextDecimal(this Random r) => (decimal)r.NextDouble();
}
