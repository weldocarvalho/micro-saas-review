namespace ServiceWorker.Infrastructure.PersistancyEntities;

public class SkinAnalysisPersistancyEntity
{
    public Guid AnalysisId { get; set; }
    public Guid PacienteId { get; set; }
    public float OverallHealthScore { get; set; }
    public string ConditionsJson { get; set; } = string.Empty;
    public string RecommendationsJson { get; set; } = string.Empty;
    public DateTime AnalyzedAt { get; set; }
}