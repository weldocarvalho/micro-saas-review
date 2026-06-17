using MediatR;
using Microsoft.Extensions.Logging;
using ServiceWorker.Application.Commands.Requests;
using ServiceWorker.Domain.Services;

namespace ServiceWorker.Application.Commands.Handlers;

public class AnalyzeSkinCommandHandler : IRequestHandler<AnalyzeSkinCommand, AnalyzeSkinCommandResult>
{
    private readonly ISkinAnalysisService _skinAnalysisService;
    private readonly IAnalysisRepository _analysisRepository;
    private readonly IPublisher _publisher;
    private readonly ILogger<AnalyzeSkinCommandHandler> _logger;

    public AnalyzeSkinCommandHandler(
        ISkinAnalysisService skinAnalysisService,
        IAnalysisRepository analysisRepository,
        IPublisher publisher,
        ILogger<AnalyzeSkinCommandHandler> logger)
    {
        _skinAnalysisService = skinAnalysisService;
        _analysisRepository = analysisRepository;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task<AnalyzeSkinCommandResult> Handle(AnalyzeSkinCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Iniciando análise de pele. CorrelationId: {CorrelationId}, PatientId: {PatientId}",
            request.CorrelationId, request.PatientId);

        try
        {
            // TODO: Fetch and process images from PhotoUrls if available
            // For now, using empty byte array as placeholder
            var imageData = Array.Empty<byte>();

            var analysisResult = await _skinAnalysisService.AnalyzeSkinAsync(
                imageData,
                request.PatientId,
                cancellationToken);

            // Save analysis result to database via repository
            var saved = await _analysisRepository.SaveAnalysisAsync(analysisResult, cancellationToken);

            _logger.LogInformation(
                "Análise de pele concluída com sucesso. CorrelationId: {CorrelationId}, AnalysisId: {AnalysisId}",
                request.CorrelationId, analysisResult.AnalysisId);

            // Publish domain event for downstream handlers (e.g., notifications, scheduling)
            await _publisher.Publish(
                new SkinAnalysisCompletedEvent(
                    analysisResult.AnalysisId,
                    request.PatientId,
                    request.CorrelationId,
                    analysisResult),
                cancellationToken);

            return new AnalyzeSkinCommandResult
            {
                AnalysisId = analysisResult.AnalysisId,
                PatientId = analysisResult.PacienteId,
                Conditions = analysisResult.Conditions,
                OverallHealthScore = analysisResult.OverallHealthScore,
                Recommendations = analysisResult.Recommendations,
                AnalyzedAt = analysisResult.AnalyzedAt
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Erro ao processar análise de pele. CorrelationId: {CorrelationId}, PatientId: {PatientId}",
                request.CorrelationId, request.PatientId);
            throw;
        }
    }
}
