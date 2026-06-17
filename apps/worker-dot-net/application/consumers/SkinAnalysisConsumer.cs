using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using ServiceWorker.Application.Commands.Requests;
using ServiceWorker.Application.Models;

namespace ServiceWorker.Application.Consumers;

public class SkinAnalysisConsumer : IConsumer<InitiateSkinAnalysisEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<SkinAnalysisConsumer> _logger;

    public SkinAnalysisConsumer(IMediator mediator, ILogger<SkinAnalysisConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<InitiateSkinAnalysisEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation(
            "Recebido evento de análise de pele. CorrelationId: {CorrelationId}, PatientId: {PatientId}",
            message.CorrelationId, message.PatientId);

        try
        {
            var command = new AnalyzeSkinCommand
            {
                PatientId = message.PatientId,
                SkinType = message.SkinType,
                SkinConcerns = message.SkinConcerns,
                BodyArea = message.BodyArea,
                CorrelationId = message.CorrelationId,
                RequestedAt = message.RequestedAt,
                PhotoUrls = message.PhotoUrls
            };

            var result = await _mediator.Send(command, context.CancellationToken);

            _logger.LogInformation(
                "Análise processada com sucesso. CorrelationId: {CorrelationId}, AnalysisId: {AnalysisId}",
                message.CorrelationId, result.AnalysisId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Erro ao processar análise de pele. CorrelationId: {CorrelationId}",
                message.CorrelationId);
            throw;
        }
    }
}
