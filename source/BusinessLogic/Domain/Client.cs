namespace source.BusinessLogic.Domain;

public enum ClientState
{
    Active = 1,
    Inactive,
    Suspended,
}

public class Client
{
    public Guid Id { get; } = Guid.NewGuid();
    public ClientState State { get; set; } = ClientState.Active;
    public required string CompanyName { get; set; }
    public required string Address { get; set; }
    public required string Billing { get; set; }
    public required string Password { get; set; }
}
