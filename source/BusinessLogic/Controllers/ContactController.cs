using source.BusinessLogic.Domain;
using source.BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;

namespace source.BusinessLogic.Controllers;

[ApiController]
[Route("api/contact")]
public class ContactController(ContactService contactService) : ControllerBase
{
    private readonly ContactService _contactService = contactService;

    [HttpPost]
    public IActionResult Create(CreateContactRequest request)
    {
        // mapping to internal representation
        var contact = request.ToDomain();

        // invoking the use case
        //_contactService.Create(contact);

        // mapping to external representation
        return CreatedAtAction(
            actionName: nameof(this.Get),
            routeValues: new { contact.Id },
            value: ContactResponse.FromDomain(contact)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        // invoking the use case
        var contact = await _contactService.Get(id);

        // return 200 ok response
        return contact is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Contact not found")
            : Ok(ContactResponse.FromDomain(contact));
    }
}

public record CreateContactRequest(Guid ClientId, string Name, string Surname, string PhoneNumber, string Email)
{
    public Contact ToDomain()
    {
        return new Contact
        {
            ClientId = ClientId,
            Name = Name,
            Surname = Surname,
            PhoneNumber = PhoneNumber,
            Email = Email,
        };
    }
}

public record ContactResponse(Guid Id, Guid ClientId, string Name, string Surname, string PhoneNumber, string Email)
{
    public static ContactResponse FromDomain(Contact contact)
    {
        return new ContactResponse(
            contact.Id,
            contact.ClientId,
            contact.Name,
            contact.Surname,
            contact.PhoneNumber,
            contact.Email
        );
    }
}