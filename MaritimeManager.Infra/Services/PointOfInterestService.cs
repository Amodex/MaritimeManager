using MaritimeManager.App.Interfaces;
using MaritimeManager.Domain.Entities;
using MaritimeManager.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace MaritimeManager.Infra.Services;

public class PointOfInterestService : IPointOfInterestService
{
    private readonly MaritimeDbContext _dbContext;
    
    public PointOfInterestService(MaritimeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<PointOfInterest>> GetAllAsync()
    {
        return await _dbContext.PointsOfInterest.ToListAsync();
    }
    
    public async Task<PointOfInterest?> GetByIdAsync(int id)
    {
        return await _dbContext.PointsOfInterest.FindAsync(id);
    }
    
    public async Task<PointOfInterest> CreateAsync(PointOfInterest pointOfInterest)
    {
        _dbContext.PointsOfInterest.Add(pointOfInterest);
        await _dbContext.SaveChangesAsync();
        
        return pointOfInterest;
    }
    
    public async Task UpdateAsync(PointOfInterest pointOfInterest)
    {
        _dbContext.Entry(pointOfInterest).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var pointOfInterest = await _dbContext.PointsOfInterest.FindAsync(id);
        if (pointOfInterest != null)
        {
            _dbContext.PointsOfInterest.Remove(pointOfInterest);
            await _dbContext.SaveChangesAsync();
        }
    }
}