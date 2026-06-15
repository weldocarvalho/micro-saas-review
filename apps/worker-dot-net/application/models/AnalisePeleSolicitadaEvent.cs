namespace DermePlan.Worker.Application.Models;

public record AnalisePeleSolicitadaEvent
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();
    public Guid PacienteId { get; init; }
    public Guid AnaliseId { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
    public DateTime SolicitadoEm { get; init; } = DateTime.UtcNow;
}
