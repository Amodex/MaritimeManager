using MaritimeManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaritimeManager.Infra.Extensions;

public static class DbContextExtensions
{
    public static async Task<PointOfInterest?> FindByIdentifierAsync(this DbSet<PointOfInterest> dbSet, Ulid identifier)
    {
        return await dbSet.FirstOrDefaultAsync(p => p.Identifier == identifier);
    }
}