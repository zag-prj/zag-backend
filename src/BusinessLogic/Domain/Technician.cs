namespace BusinessLogic.Domain;

public enum TechnicianRole
{
    Junior = 1,
    Senior = 2,
}

public enum TechnicianState
{
    Available,
    Unavailable,
    Suspended,
}

public class Technician
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public TechnicianState State { get; set; } = TechnicianState.Available;
    public required TechnicianRole Role { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public DateTime Hired { get; set; } = DateTime.Now;
    public required decimal Salary { get; set; }
}