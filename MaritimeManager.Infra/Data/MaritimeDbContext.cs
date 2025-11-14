using MaritimeManager.Domain.Entities;
using MaritimeManager.Infra.Converters;
using Microsoft.EntityFrameworkCore;

namespace MaritimeManager.Infra.Data;

public class MaritimeDbContext : DbContext
{
    public DbSet<PointOfInterest> PointsOfInterest { get; set; }
    
    public MaritimeDbContext(DbContextOptions<MaritimeDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PointOfInterest>(entity =>
        {
            entity.Property(e => e.Identifier).HasConversion<UlidToStringConverter>();
        });
    }
}