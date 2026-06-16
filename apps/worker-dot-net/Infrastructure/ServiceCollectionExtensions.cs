using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DermePlan.Worker.Domain.Services;
using DermePlan.Worker.Infrastructure.Data;
using DermePlan.Worker.Infrastructure.Repositories;
using DermePlan.Worker.Infrastructure.Services;

namespace DermePlan.Worker.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = GetConnectionString(configuration);

        services.AddDbContext<DermePlanDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IAnalysisRepository, EfAnalysisRepository>();

        services.AddScoped<IImageProcessorService, MockImageProcessorService>();
        services.AddScoped<ISkinAnalysisService, MockSkinAnalysisService>();

        return services;
    }

    private static string GetConnectionString(IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTIONSTRING");
        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            return connectionString;
        }

        connectionString = configuration.GetSection("Database")["ConnectionString"];
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Database connection string is not configured. Configure Database:ConnectionString or DATABASE_CONNECTIONSTRING.");
        }

        return connectionString;
    }
}