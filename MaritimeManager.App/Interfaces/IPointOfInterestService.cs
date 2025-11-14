using MaritimeManager.Domain.Entities;

namespace MaritimeManager.App.Interfaces;

public interface IPointOfInterestService
{
    Task<IEnumerable<PointOfInterest>> GetAllAsync();
    Task<PointOfInterest?> GetByIdAsync(int id);
    Task<PointOfInterest> CreateAsync(PointOfInterest pointOfInterest);
    Task UpdateAsync(PointOfInterest pointOfInterest);
    Task DeleteAsync(int id);
}