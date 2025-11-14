namespace MaritimeManager.Domain.Entities;

public class PointOfInterest
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public required string Type { get; set; }
}