using MassTransit;
using Microsoft.Extensions.Logging;
using DermePlan.Worker.Application.Models;

namespace DermePlan.Worker.Application.Consumers;

public class AnalisePeleConsumer : IConsumer<AnalisePeleSolicitadaEvent>
{
    private readonly ILogger<AnalisePeleConsumer> _logger;

    public AnalisePeleConsumer(ILogger<AnalisePeleConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<AnalisePeleSolicitadaEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation(
            "Recebido evento de análise de pele. CorrelationId: {CorrelationId}, PacienteId: {PacienteId}",
            message.CorrelationId, message.PacienteId);

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
