using System.ComponentModel.DataAnnotations;
using MaritimeManager.Domain.Enums;

namespace MaritimeManager.App.DTOs;

public class PointOfInterestDto
{
    [Required]
    public Ulid? Identifier { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
    public PointOfInterestType Type { get; set; }
}

public class CreatePointOfInterestDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters long")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string? Description { get; set; }
    
    [Required]
    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
    public double Latitude { get; set; }
    
    [Required]
    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
    public double Longitude { get; set; }
    
    [Required(ErrorMessage = "A type is required.")]
    public PointOfInterestType Type { get; set; }
}

public class UpdatePointOfInterestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
    public PointOfInterestType Type { get; set; }
}
