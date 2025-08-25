using LastMinuteLite.Shared;
using LastMinuteLite.Web.Components;
using LastMinuteLite.Web.Data;
using LastMinuteLite.Web.Entities;
using LastMinuteLite.Web.Models;
using LastMinuteLite.Web.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.AddServiceDefaults();

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

var app = builder.Build();

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

