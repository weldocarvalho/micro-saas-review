using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceWorker.Domain.Services;
using ServiceWorker.Infrastructure.Data;
using ServiceWorker.Infrastructure.Repositories;
using ServiceWorker.Infrastructure.Services;

namespace ServiceWorker.Infrastructure;

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