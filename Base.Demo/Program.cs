using Base.Demo.Components;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSerilog(config =>
{
    config.ReadFrom.Configuration(builder.Configuration);
});

//#region SQL 
//builder.Services.AddDbContext<AppDbContext>(option =>
//{
//    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});
////builder.Services.AddScoped<AppDbContext>();
//#endregion SQL 

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//SeedDataIfNeeded(app);

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
