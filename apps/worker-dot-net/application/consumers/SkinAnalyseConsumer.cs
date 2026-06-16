using MassTransit;
using Microsoft.Extensions.Logging;
using DermePlan.Worker.Application.Models;

namespace DermePlan.Worker.Application.Consumers;

public class SkinAnalyseConsumer : IConsumer<SkinWizardSubmittedEvent>
{
    private readonly ILogger<SkinAnalyseConsumer> _logger;

    public SkinAnalyseConsumer(ILogger<SkinAnalyseConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SkinWizardSubmittedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation(
            "Recebido evento de análise de pele. CorrelationId: {CorrelationId}, UserId: {UserId}",
            message.CorrelationId, message.UserId);

            Console.WriteLine($"Processando análise de pele para UserId: {message.UserId}, SkinType: {message.SkinType}, SkinConcerns: {message.SkinConcerns}");

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
