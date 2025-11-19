using AutoMapper;
using MaritimeManager.App.DTOs;
using MaritimeManager.App.Interfaces;
using MaritimeManager.Domain.Entities;
using MaritimeManager.Infra.Data;
using MaritimeManager.Infra.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MaritimeManager.Infra.Services;

public class PointOfInterestService : IPointOfInterestService
{
    private readonly MaritimeDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public PointOfInterestService(MaritimeDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<PointOfInterestDto>> GetAllAsync()
    {
        IEnumerable<PointOfInterest> pointOfInterests = await _dbContext.PointsOfInterest.ToListAsync();
        return _mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterests);
    }
    
    public async Task<PointOfInterestDto?> GetByIdentifierAsync(Ulid identifier)
    {
        PointOfInterest? pointOfInterest = await _dbContext.PointsOfInterest.FindByIdentifierAsync(identifier);

        if (pointOfInterest == null) return null;
        
        return _mapper.Map<PointOfInterestDto>(pointOfInterest);
    }
    
    public async Task<PointOfInterestDto> CreateAsync(CreatePointOfInterestDto pointOfInterestDto)
    {
        PointOfInterest pointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterestDto);
        
        _dbContext.PointsOfInterest.Add(pointOfInterest);
        await _dbContext.SaveChangesAsync();
        
        return _mapper.Map<PointOfInterestDto>(pointOfInterest);
    }
    
    public async Task UpdateAsync(Ulid identifier, UpdatePointOfInterestDto pointOfInterestDto)
    {
        PointOfInterest? pointOfInterest = await _dbContext.PointsOfInterest.FindByIdentifierAsync(identifier);
        
        if (pointOfInterest == null) throw new KeyNotFoundException($"Pointofinterest with identifier {identifier} not found");
        
        _mapper.Map(pointOfInterestDto, pointOfInterest);

        _dbContext.Entry(pointOfInterest).State = EntityState.Modified;
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Ulid identifier)
    {
        var pointOfInterest = await _dbContext.PointsOfInterest.FindByIdentifierAsync(identifier);
        if (pointOfInterest != null)
        {
            _dbContext.PointsOfInterest.Remove(pointOfInterest);
            await _dbContext.SaveChangesAsync();
        }
    }
}