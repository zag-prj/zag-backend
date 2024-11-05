namespace source.BusinessLogic.Domain;

public class Contact
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid ClientId { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
}