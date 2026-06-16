namespace DermePlan.Worker.Application.Models;

public record SkinWizardSubmittedEvent
{
    public Guid UserId { get; init; }
    public string SkinType { get; init; } = string.Empty;
    public string SkinConcerns { get; init; } = string.Empty;
    public Guid CorrelationId { get; init; }
    public DateTime SolicitadoEm { get; init; }
}

