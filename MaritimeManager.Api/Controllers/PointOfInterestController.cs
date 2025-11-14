using MaritimeManager.App.Interfaces;
using MaritimeManager.Domain.Entities;
using MaritimeManager.Infra.Services;
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
    public async Task<ActionResult<IEnumerable<PointOfInterest>>> GetAll()
    {
        return Ok(await _pointOfInterestService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PointOfInterest>> Get(int id)
    {
        PointOfInterest? pointOfInterest = await _pointOfInterestService.GetByIdAsync(id);

        if (pointOfInterest == null) return NotFound();
        
        return Ok(pointOfInterest);
    }

    [HttpPost]
    public async Task<ActionResult<PointOfInterest>> Post(PointOfInterest pointOfInterest)
    {
        PointOfInterest createdPointOfInterest = await _pointOfInterestService.CreateAsync(pointOfInterest);
        
        return CreatedAtAction(nameof(Get), new { id = pointOfInterest.Id }, createdPointOfInterest);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PointOfInterest pointOfInterest)
    {
        if (id != pointOfInterest.Id) return BadRequest();
        
        await _pointOfInterestService.UpdateAsync(pointOfInterest);
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _pointOfInterestService.DeleteAsync(id);
        return NoContent();
    }
}