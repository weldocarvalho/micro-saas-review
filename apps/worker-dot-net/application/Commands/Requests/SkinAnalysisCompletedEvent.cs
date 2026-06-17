using MediatR;
using ServiceWorker.Domain.Services;

namespace ServiceWorker.Application.Commands.Requests;

public record SkinAnalysisCompletedEvent(
    Guid AnalysisId,
    Guid PatientId,
    Guid CorrelationId,
    SkinAnalysisResult Result) : INotification;
