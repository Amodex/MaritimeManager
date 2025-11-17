using MaritimeManager.App.DTOs;
using MaritimeManager.App.Interfaces;
using MaritimeManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MaritimeManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class PointOfInterestController : ControllerBase
{
    private readonly IPointOfInterestService _pointOfInterestService;

    public PointOfInterestController(IPointOfInterestService pointOfInterestService)
    {
        _pointOfInterestService = pointOfInterestService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetAll()
    {
        return Ok(await _pointOfInterestService.GetAllAsync());
    }

    [HttpGet("{identifier}")]
    public async Task<ActionResult<PointOfInterest>> Get(Ulid identifier)
    {
        PointOfInterestDto? pointOfInterest = await _pointOfInterestService.GetByIdentifierAsync(identifier);

        if (pointOfInterest == null) return NotFound();
        
        return Ok(pointOfInterest);
    }

    [HttpPost]
    public async Task<ActionResult<PointOfInterest>> Post(CreatePointOfInterestDto createPointOfInterestDto)
    {
        PointOfInterestDto createdPointOfInterest = await _pointOfInterestService.CreateAsync(createPointOfInterestDto);
        
        return Ok(createdPointOfInterest);
    }
    
    [HttpPut("{identifier}")]
    public async Task<IActionResult> Put(Ulid identifier, UpdatePointOfInterestDto updatePointOfInterestDto)
    {
        try
        {
            await _pointOfInterestService.UpdateAsync(identifier, updatePointOfInterestDto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpDelete("{identifier}")]
    public async Task<IActionResult> Delete(Ulid identifier)
    {
        await _pointOfInterestService.DeleteAsync(identifier);
        
        return NoContent();
    }
}