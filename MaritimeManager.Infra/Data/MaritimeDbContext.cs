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
            entity.Property(e => e.Id).HasColumnOrder(0);
            entity.Property(e => e.Identifier).HasColumnOrder(1);
            entity.Property(e => e.Name).HasColumnOrder(2);
            entity.Property(e => e.Description).HasColumnOrder(3);
            entity.Property(e => e.Latitude).HasColumnOrder(4);
            entity.Property(e => e.Longitude).HasColumnOrder(5);
            entity.Property(e => e.Type).HasColumnOrder(6);
            entity.Property(e => e.Identifier).HasConversion<UlidToStringConverter>();
        });
    }
}