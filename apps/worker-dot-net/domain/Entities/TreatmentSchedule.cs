namespace ServiceWorker.Domain.Entities;

public class TreatmentSchedule
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PatientId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string Status { get; private set; } = "PROCESSING"; // Processing, Completed, Failed
    public List<ScheduleItem> Items { get; private set; } = new();

    // Required for EF Core
    private TreatmentSchedule() { }

    public TreatmentSchedule(Guid patientId)
    {
        PatientId = patientId;
    }

    public void CompleteWithSuccess(List<ScheduleItem> items)
    {
        Items = items;
        Status = "COMPLETED";
    }
}

public class ScheduleItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public int ScheduleDay { get; private set; } // e.g., Day 1, Day 2
    public string RecommendedProduct { get; private set; } = string.Empty;
    public string UsageInstruction { get; private set; } = string.Empty; // e.g., Apply with circular massage for 5 min
}
