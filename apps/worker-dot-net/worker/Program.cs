using MassTransit;
using Serilog;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using DermePlan.Worker.Domain.Services;
using DermePlan.Worker.Infra.Services;
using DermePlan.Worker.Application.Consumers;

var builder = Host.CreateDefaultBuilder(args)
    .UseSerilog((context, configuration) =>
    {
        configuration
            .MinimumLevel.Information()
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(
                "logs/worker-.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");

        if (context.HostingEnvironment.IsDevelopment())
        {
            configuration.MinimumLevel.Debug();
        }
    })
    .ConfigureServices((context, services) =>
    {
        var isDevelopment = context.HostingEnvironment.IsDevelopment();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<SkinAnalyseConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                var logger = context.GetRequiredService<ILogger<Program>>();
                var environment = context.GetRequiredService<IHostEnvironment>();

                logger.LogInformation("IsDevelopment = {IsDevelopment}; Environment = {EnvironmentName}", isDevelopment, environment.EnvironmentName);
                logger.LogDebug("UsingRabbitMq context type: {ContextType}", context.GetType().FullName);

                // Se a variável de ambiente existir (ex: em produção/Docker), usa ela.
                // Caso contrário, lê a seção do appsettings.json
                var rabbitMqUri = Environment.GetEnvironmentVariable("RABBITMQ_URI");

                if (!string.IsNullOrEmpty(rabbitMqUri))
                {
                    logger.LogInformation("Using RabbitMQ URI from environment variable: {RabbitMqUri}", rabbitMqUri);
                    cfg.Host(new Uri(rabbitMqUri));
                }
                else
                {
                    // Busca os dados estruturados do seu appsettings.json
                    var rabbitSettings = context.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>().GetSection("RabbitMQ");
                    var rabbitHost = rabbitSettings["Host"] ?? "localhost";
                    var rabbitUsername = rabbitSettings["Username"] ?? "guest";

                    logger.LogInformation("Using RabbitMQ settings from appsettings: Host={RabbitHost}, Username={RabbitUsername}", rabbitHost, rabbitUsername);

                    cfg.Host(rabbitHost, "/", h =>
                    {
                        h.Username(rabbitUsername);
                        h.Password(rabbitSettings["Password"] ?? "guest");
                    });
                }

                cfg.ConfigureEndpoints(context);
            });
        });


        // Register services
        services.AddScoped<IImageProcessorService, MockImageProcessorService>();
        services.AddScoped<ISkinAnalysisService, MockSkinAnalysisService>();
        services.AddScoped<IAnalysisRepository, MockAnalysisRepository>();

        // Health checks
        services.AddHealthChecks();

        services.AddHostedService<WorkerService>();
    });

var host = builder.Build();
await host.RunAsync();

public class WorkerService : BackgroundService
{
    private readonly ILogger<WorkerService> _logger;
    private readonly IBusControl _busControl;

    public WorkerService(ILogger<WorkerService> logger, IBusControl busControl)
    {
        _logger = logger;
        _busControl = busControl;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("🚀 Vamos ver se está aqui... DermePlan Worker iniciado - aguardando mensagens...");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await _busControl.StartAsync(cancellationToken);
        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _busControl.StopAsync(cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}
