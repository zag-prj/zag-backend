using source.BusinessLogic.Domain;
using source.BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;

namespace source.BusinessLogic.Controllers;

[ApiController]
[Route("api/contract")]
public class ContractController(ContractService service) : ControllerBase
{
    private readonly ContractService _service = service;

    [HttpPost]
    public IActionResult Create(CreateContractRequest request)
    {
        // mapping to internal representation
        var contract = request.ToDomain();

        // invoking the use case
        _service.Create(contract);

        // mapping to external representation
        return CreatedAtAction(
            actionName: nameof(this.Get),
            routeValues: new { contract.Id },
            value: ContractResponse.FromDomain(contract)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        // invoking the use case
        var contract = _service.Get(id);

        // return 200 ok respone
        return contract is null
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Contract not found")
        : Ok(ContractResponse.FromDomain(contract));
    }
}

public record CreateContractRequest(
    Guid ClientId,
    DateTime Until,
    decimal PriceMonthly
)
{
    public Contract ToDomain()
    {
        return new Contract
        {
            ClientId = ClientId,
            Until = Until,
            PriceMonthly = PriceMonthly,
        };
    }

}

public record ContractResponse(
    Guid Id,
    ContractState State,
    DateTime Issued,
    DateTime Until,
    decimal PriceMonthly
)
{
    public static ContractResponse FromDomain(Contract contract)
    {
        return new ContractResponse(
            contract.Id,
            contract.State,
            contract.Issued,
            contract.Until,
            contract.PriceMonthly
        );
    }
}