// Program.cs (Hotel API)
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

// Program.cs (Hotel API) – Random-Deal mit optionalem ?date=YYYY-MM-DD
app.MapGet("/api/deals/random", (DateOnly? date) =>
{
    var hotels = new[] { "Hotel Aurora", "Grand Vista", "Sea Breeze", "Mountain Inn" };
    var cities = new[] { "Zürich", "Genf", "London", "Frankfurt", "Paris", "New York", "Barcelona", "Madrid" };
    var rnd = Random.Shared;

    // Wenn date gesetzt: nehme diesen Check-in-Tag (14:00 Uhr UTC),
    // sonst ein zufälliges Datum in den nächsten 30 Tagen.
    var checkInUtc = date.HasValue
        ? new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 14, 0, 0, DateTimeKind.Utc)
        : DateTime.UtcNow.AddDays(rnd.Next(1, 30));

    var deal = new HotelDealDto(
        Id: Guid.NewGuid(),
        HotelName: hotels[rnd.Next(hotels.Length)],
        City: cities[rnd.Next(cities.Length)],
        Stars: rnd.Next(2, 6),
        CheckInUtc: checkInUtc,
        Nights: rnd.Next(1, 7),
        PricePerNight: Math.Round((decimal)rnd.Next(49, 299) + (decimal)rnd.NextDouble(), 2),
        RoomsLeft: rnd.Next(1, 8)
    );

    return Results.Ok(deal);
})
.WithName("GetRandomHotelDeal");

app.UseSwagger();
app.UseSwaggerUI();

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
