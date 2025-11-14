using MaritimeManager.Infra.Data.Seeders;

namespace MaritimeManager.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task<IApplicationBuilder> SeedDatabaseAsync(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var seeder = serviceScope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
        
        await seeder.SeedAsync();
        
        return app;
    }
}