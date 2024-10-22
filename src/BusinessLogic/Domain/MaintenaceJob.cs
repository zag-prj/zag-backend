namespace BusinessLogic.Domain;

public enum MaintenanceJobState
{
    Pending = 1,
    Ongoing,
    Completed,
}

public class MaintenanceJob
{
    public required Guid ClientId { get; set; }
    public required Guid TechnicianId { get; set; }
    public MaintenanceJobState State { get; set; } = MaintenanceJobState.Pending;
    public DateTime Requested { get; set; } = DateTime.Now;
    public DateTime? Started { get; set; } = null;
    public DateTime? Completed { get; set; } = null;
}
