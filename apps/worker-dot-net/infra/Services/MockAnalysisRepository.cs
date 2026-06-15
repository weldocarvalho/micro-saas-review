using Microsoft.Extensions.Logging;
using DermePlan.Worker.Domain.Services;

namespace DermePlan.Worker.Infra.Services;

public class MockAnalysisRepository : IAnalysisRepository
{
    private readonly ILogger<MockAnalysisRepository> _logger;
    private static readonly Dictionary<Guid, SkinAnalysisResult> _store = new();

    public MockAnalysisRepository(ILogger<MockAnalysisRepository> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SaveAnalysisAsync(SkinAnalysisResult result, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Salvando análise (MOCK): {AnalysisId} para paciente {PacienteId}", 
            result.AnalysisId, result.PacienteId);

        await Task.Delay(50, cancellationToken);

        _store[result.AnalysisId] = result;
        return true;
    }

    public async Task<SkinAnalysisResult?> GetAnalysisAsync(Guid analysisId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Recuperando análise (MOCK): {AnalysisId}", analysisId);

        await Task.Delay(50, cancellationToken);

        return _store.TryGetValue(analysisId, out var result) ? result : null;
    }
}
