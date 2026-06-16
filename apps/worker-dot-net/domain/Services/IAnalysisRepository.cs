namespace ServiceWorker.Domain.Services;

public interface IAnalysisRepository
{
    Task<bool> SaveAnalysisAsync(SkinAnalysisResult result, CancellationToken cancellationToken = default);
    Task<SkinAnalysisResult?> GetAnalysisAsync(Guid analysisId, CancellationToken cancellationToken = default);
}
