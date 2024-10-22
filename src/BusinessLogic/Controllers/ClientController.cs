using BusinessLogic.Domain;
using BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;

namespace BusinessLogic.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController(ClientService service) : ControllerBase
{
    private readonly ClientService _service = service;

    [HttpPost]
    public IActionResult Create(CreateClientRequest request)
    {
        // mapping to internal representation
        var client = request.ToDomain();

        // invoking the use case
        _service.Create(client);

        // mapping to external representation
        return CreatedAtAction(
            actionName: nameof(this.Get),
            routeValues: new { client.Id },
            value: ClientResponse.FromDomain(client)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        // invoking the use case
        var client = _service.Get(id);

        // return 200 ok response
        return client is null
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Product not found")
        : Ok(ClientResponse.FromDomain(client));
    }
}

public record CreateClientRequest(
    string CompanyName,
    string Address,
    string Billing,
    string Password
)
{
    public Client ToDomain()
    {
        return new Client
        {
            CompanyName = CompanyName,
            Address = Address,
            Billing = Billing,
            Password = Password,
        };
    }
}

public record ClientResponse(
    Guid Id,
    ClientState State,
    string CompanyName,
    string Address,
    string Billing
)
{
    public static ClientResponse FromDomain(Client client)
    {
        return new ClientResponse(
            client.Id,
            client.State,
            client.CompanyName,
            client.Address,
            client.Billing
        );
    }
}