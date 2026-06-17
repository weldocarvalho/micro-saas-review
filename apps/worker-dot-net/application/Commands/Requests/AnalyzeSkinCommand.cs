using MediatR;
using ServiceWorker.Domain.Services;

namespace ServiceWorker.Application.Commands.Requests;

public record AnalyzeSkinCommand : IRequest<AnalyzeSkinCommandResult>
{
    public required Guid PatientId { get; init; }
    public required string SkinType { get; init; }
    public required string SkinConcerns { get; init; }
    public required string BodyArea { get; init; }
    public required Guid CorrelationId { get; init; }
    public required DateTime RequestedAt { get; init; }
    public required List<string> PhotoUrls { get; init; }
}

public record AnalyzeSkinCommandResult
{
    public required Guid AnalysisId { get; init; }
    public required Guid PatientId { get; init; }
    public required SkinCondition[] Conditions { get; init; }
    public required float OverallHealthScore { get; init; }
    public required string[] Recommendations { get; init; }
    public required DateTime AnalyzedAt { get; init; }
}
