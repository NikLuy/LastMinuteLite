using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using LastMinuteLite.Web.Entities;

namespace LastMinuteLite.Web.Data;

public class AppDbContext : DbContext
{
    public DbSet<ComboDeal> ComboDeals => Set<ComboDeal>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ComboDeal configuration (flattened snapshot of flight + hotel values)
        modelBuilder.Entity<ComboDeal>(e =>
        {
            e.Property(p => p.Total).HasColumnType("decimal(18,2)");
            e.Property(p => p.FlightPrice).HasColumnType("decimal(18,2)");
            e.Property(p => p.HotelPricePerNight).HasColumnType("decimal(18,2)");
        });
    }
}
