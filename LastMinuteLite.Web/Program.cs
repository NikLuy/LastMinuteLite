using LastMinuteLite.Web.Components;
using LastMinuteLite.Web.Data;
using LastMinuteLite.Web.Models;
using LastMinuteLite.Web.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using LastMinuteLite.Web.Entities;
using LastMinuteLite.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSerilog(config =>
{
    config.ReadFrom.Configuration(builder.Configuration);
});

#region SQL 
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion SQL 


// Options binden
builder.Services.Configure<ServiceEndpointsOptions>(
    builder.Configuration.GetSection("Services"));

// Named/Typed HttpClients mit Token-Header
builder.Services.AddHttpClient<IAirplaneService, AirplaneService>("Airplane",
    (sp, http) =>
    {
        var opts = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<ServiceEndpointsOptions>>().Value.Airplane;
        http.BaseAddress = new Uri(opts.BaseUrl);
        if (!string.IsNullOrWhiteSpace(opts.ApiToken))
            http.DefaultRequestHeaders.Add("X-Api-Token", opts.ApiToken);
    });

builder.Services.AddHttpClient<IHotelService, HotelService>("Hotel",
    (sp, http) =>
    {
        var opts = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<ServiceEndpointsOptions>>().Value.Hotel;
        http.BaseAddress = new Uri(opts.BaseUrl);
        if (!string.IsNullOrWhiteSpace(opts.ApiToken))
            http.DefaultRequestHeaders.Add("X-Api-Token", opts.ApiToken);
    });

builder.Services.AddScoped<IComboDealService, ComboDealService>();

var app = builder.Build();

// Minimal API endpoints for combo deals (persisted)
app.MapPost("/api/combos", async (ComboDealDto dto, AppDbContext db) =>
{
    var entity = ComboDeal.FromDto(dto);
    db.ComboDeals.Add(entity);
    await db.SaveChangesAsync();
    return Results.Created($"/api/combos/{entity.Id}", entity.Id);
});

app.MapGet("/api/combos", async (AppDbContext db) =>
{
    var list = await db.ComboDeals
        .OrderByDescending(c => c.CreatedUtc)
        .Take(50)
        .Select(c => c.ToDto())
        .ToListAsync();
    return Results.Ok(list);
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();


//Funktion f�r Migration & Seeding
//static void SeedDataIfNeeded(WebApplication app)
//{
//    using var scope = app.Services.CreateScope();
//    var services = scope.ServiceProvider;

//    try
//    {
//        var db = services.GetRequiredService<AppDbContext>();
//        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("Seeding");

//        db.Database.Migrate(); // optional: Migration sicherstellen
//        DbInitializer.Seed(db, logger);
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("Seeding");
//        logger.LogError(ex, "Fehler beim Seeding");
//        throw;
//    }
//}
