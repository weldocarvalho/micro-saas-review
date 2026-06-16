namespace ServiceWorker.Domain.Services;

public interface ISkinAnalysisService
{
    Task<SkinAnalysisResult> AnalyzeSkinAsync(byte[] imageData, Guid pacienteId, CancellationToken cancellationToken = default);
}

public record SkinAnalysisResult
{
    public required Guid AnalysisId { get; init; }
    public required Guid PacienteId { get; init; }
    public required SkinCondition[] Conditions { get; init; }
    public required float OverallHealthScore { get; init; }
    public required string[] Recommendations { get; init; }
    public required DateTime AnalyzedAt { get; init; }
}

public record SkinCondition
{
    public required string Name { get; init; }
    public required float Severity { get; init; }
    public required string Description { get; init; }
}
