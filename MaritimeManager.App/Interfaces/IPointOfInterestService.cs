using MaritimeManager.App.DTOs;
using MaritimeManager.Domain.Entities;

namespace MaritimeManager.App.Interfaces;

public interface IPointOfInterestService
{
    Task<IEnumerable<PointOfInterestDto>> GetAllAsync();
    Task<PointOfInterestDto?> GetByIdentifierAsync(Ulid identifier);
    Task<PointOfInterestDto> CreateAsync(CreatePointOfInterestDto pointOfInterestDto);
    Task UpdateAsync(Ulid identifier, UpdatePointOfInterestDto pointOfInterestDto);
    Task DeleteAsync(Ulid identifier);
}