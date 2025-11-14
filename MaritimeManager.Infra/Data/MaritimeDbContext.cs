using MaritimeManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaritimeManager.Infra.Data;

public class MaritimeDbContext : DbContext
{
    public MaritimeDbContext(DbContextOptions<MaritimeDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<PointOfInterest> PointsOfInterest { get; set; }
}