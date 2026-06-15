using Microsoft.Extensions.Logging;
using DermePlan.Worker.Domain.Services;

namespace DermePlan.Worker.Infra.Services;

public class MockSkinAnalysisService : ISkinAnalysisService
{
    private readonly ILogger<MockSkinAnalysisService> _logger;

    public MockSkinAnalysisService(ILogger<MockSkinAnalysisService> logger)
    {
        _logger = logger;
    }

    public async Task<SkinAnalysisResult> AnalyzeSkinAsync(byte[] imageData, Guid pacienteId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Analisando pele do paciente (MOCK): {PacienteId}", pacienteId);

        await Task.Delay(200, cancellationToken);

        return new SkinAnalysisResult
        {
            AnalysisId = Guid.NewGuid(),
            PacienteId = pacienteId,
            Conditions = new[]
            {
                new SkinCondition
                {
                    Name = "Celulite",
                    Severity = 0.35f,
                    Description = "Moderada - grau 1"
                },
                new SkinCondition
                {
                    Name = "Flacidez",
                    Severity = 0.20f,
                    Description = "Leve"
                }
            },
            OverallHealthScore = 0.65f,
            Recommendations = new[] 
            { 
                "Aumentar hidratação",
                "Tratamento regular com radiofrequência",
                "Massagem com drenagem linfática"
            },
            AnalyzedAt = DateTime.UtcNow
        };
    }
}
