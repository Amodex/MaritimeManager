using System.ComponentModel.DataAnnotations;

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
    public string Type { get; set; } = string.Empty;
}

public class CreatePointOfInterestDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
    public string Type { get; set; } = string.Empty;
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
    public string Type { get; set; } = string.Empty;
}
