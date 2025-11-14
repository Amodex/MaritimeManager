using MaritimeManager.Domain.Entities;

namespace MaritimeManager.Infra.Data.Seeders;

public class DatabaseSeeder
{
    private readonly MaritimeDbContext _dbContext;

    public DatabaseSeeder(MaritimeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();

        if (!_dbContext.PointsOfInterest.Any())
        {
            var points = new PointOfInterest[]
            {
                new PointOfInterest
                {
                    Id = 1,
                    Identifier = Ulid.NewUlid(),
                    Name = "Thorntonbank Wind Farm", 
                    Description = "The Thorntonbank Wind Farm is an offshore wind farm. It is the first offshore wind farm in Belgium.", 
                    Latitude = 51.539914859121396, 
                    Longitude = 2.9400334263154755, 
                    Type = "Energy Infrastructure"
                },
                new PointOfInterest
                {
                    Id = 2,
                    Identifier = Ulid.NewUlid(),
                    Name = "Princess Elisabeth Island", 
                    Description = "Princess Elisabeth Island is an artificial island under construction in the North Sea, with an area of six hectares.", 
                    Latitude = 51.527222, 
                    Longitude = 2.498333, 
                    Type = "Energy Infrastructure"
                },
                new PointOfInterest
                {
                    Id = 3,
                    Identifier = Ulid.NewUlid(),
                    Name = "UB-20", 
                    Description = "A German WW1-era submarine destroyed in a British minefield near Ostend.", 
                    Latitude = 51.35, 
                    Longitude = 2.633333, 
                    Type = "Wreck"
                },
                new PointOfInterest
                {
                    Id = 4,
                    Identifier = Ulid.NewUlid(),
                    Name = "LTV Westhinder", 
                    Description = "The wreck of a Belgian Lightship that sunk due to collision with a german sloop.", 
                    Latitude = 51.22878, 
                    Longitude = 2.27134, 
                    Type = "Wreck"
                },
                new PointOfInterest
                {
                    Id = 5,
                    Identifier = Ulid.NewUlid(),
                    Name = "Vliegend Hert", 
                    Description = "A Dutch VOC ship that sunk in 1735 after it sailed onto a sandbank and punctured.", 
                    Latitude = 51.491972, 
                    Longitude = 3.114556, 
                    Type = "Wreck"
                },
            };

            await _dbContext.PointsOfInterest.AddRangeAsync(points);
            await _dbContext.SaveChangesAsync();
        }
    }
}