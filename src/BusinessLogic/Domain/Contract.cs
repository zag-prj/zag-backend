namespace BusinessLogic.Domain;

public enum ContractState
{
    Valid = 1,
    Breach,
    Completed,
}

public class Contract
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ContractState State { get; set; } = ContractState.Valid;
    public required Guid ClientId { get; set; }
    public DateTime Issued { get; set; } = DateTime.Now;
    public required DateTime Until { get; set; }
    public required decimal PriceMonthly { get; set; }
}
