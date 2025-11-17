using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using MaritimeManager.Api.Extensions;
using MaritimeManager.App.DTOs;
using MaritimeManager.App.Interfaces;
using MaritimeManager.App.Mappings;
using MaritimeManager.Domain.Entities;
using MaritimeManager.Infra.Data;
using MaritimeManager.Infra.Data.Seeders;
using MaritimeManager.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MaritimeDbContext>(options
    => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(conf => { }, typeof(MappingProfile));

builder.Services.AddControllers();

builder.Services.AddScoped<IPointOfInterestService, PointOfInterestService>();
builder.Services.AddScoped<DatabaseSeeder>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    });

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Maritime Manager API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Maritime Manager API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at root
    });
}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

await app.SeedDatabaseAsync();

app.Run();
