namespace ServiceWorker.Application.Models;

public record InitiateSkinAnalysisEvent
{
    public Guid PatientId { get; init; }
    public string SkinType { get; init; } = string.Empty;
    public string SkinConcerns { get; init; } = string.Empty;
    public string BodyArea { get; init; } = string.Empty;
    public Guid CorrelationId { get; init; }
    public DateTime RequestedAt { get; init; }
    public List<string> PhotoUrls { get; init; } = [];
}

