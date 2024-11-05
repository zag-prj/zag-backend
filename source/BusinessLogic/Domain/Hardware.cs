namespace source.BusinessLogic.Domain;

public enum HardwareState
{
    Running = 1,
    Down,
    Maintenance,
}

public class Hardware
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public HardwareState State { get; set; } = HardwareState.Running;
    public required Guid ContractId { get; set; }
    public required string RefName { get; set; }
    public required decimal Value { get; set; }
}

public class Spec
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string RefName { get; set; }
    public required decimal Value { get; set; }
}

public class HardwareSpec
{
    public required Guid HardwareId { get; set; }
    public required Guid SpecId { get; set; }
    public required int Count { get; set; }
}
