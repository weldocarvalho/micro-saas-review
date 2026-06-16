using MassTransit;
using Microsoft.Extensions.Logging;
using ServiceWorker.Application.Models;

namespace ServiceWorker.Application.Consumers;

public class SkinAnalysisConsumer : IConsumer<InitiateSkinAnalysisEvent>
{
    private readonly ILogger<SkinAnalysisConsumer> _logger;

    public SkinAnalysisConsumer(ILogger<SkinAnalysisConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<InitiateSkinAnalysisEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation(
            "Recebido evento de análise de pele. CorrelationId: {CorrelationId}, UserId: {UserId}",
            message.CorrelationId, message.PatientId);

            Console.WriteLine($"Processando análise de pele para UserId: {message.PatientId}, SkinType: {message.SkinType}, SkinConcerns: {message.SkinConcerns}");

        try
        {
            // TODO: Aqui entrará a chamada para a IA e Banco de Dados nos próximos marcos
            await Task.Delay(500); // Simulando processamento rápido

            _logger.LogInformation(
                "Análise processada com sucesso. CorrelationId: {CorrelationId}",
                message.CorrelationId);
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
