using BusinessLogic.Domain;
using BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;

namespace BusinessLogic.Controllers;

[ApiController]
[Route("api/hardware")]
public class HardwareController(HardwareService service) : ControllerBase
{
    private readonly HardwareService _service = service;

    [HttpPost]
    public IActionResult Create(CreateHardwareRequest request)
    {
        // mapping to internal representation
        var hardware = request.ToDomain();

        // invoking the use case
        _service.Create(hardware);

        // mapping to external representation
        return CreatedAtAction(
            actionName: nameof(this.Get),
            routeValues: new { hardware.Id },
            value: HardwareResponse.FromDomain(hardware)
        );
    }

    [HttpGet("id:guid")]
    public IActionResult Get(Guid id)
    {
        // invoking the use case
        Hardware? hardware = _service.Get(id);

        // return 200 ok response
        return hardware is null
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Hardware not found")
        : Ok(HardwareResponse.FromDomain(hardware));
    }
}

public record CreateHardwareRequest(
    Guid ContractId,
    string RefName,
    decimal Value
)
{
    public Hardware ToDomain()
    {
        return new Hardware
        {
            ContractId = ContractId,
            RefName = RefName,
            Value = Value,
        };
    }
}

public record HardwareResponse(
    Guid Id,
    HardwareState State,
    Guid ContractId,
    string RefName,
    decimal Value
)
{
    public static HardwareResponse FromDomain(Hardware hardware)
    {
        return new HardwareResponse(
            hardware.Id,
            hardware.State,
            hardware.ContractId,
            hardware.RefName,
            hardware.Value
        );
    }
}


[ApiController]
[Route("api/hardware/spec")]
public class SpecController(SpecService service) : ControllerBase {
    private readonly SpecService _service = service;

    public IActionResult Create(CreateSpecRequest request) {}
}

public record CreateSpecRequest;

public record SpecResponse;

// TODO: decide on endpoint path
// [ApiController]
// [Route("api/client")]
public class HardwareSpecController : ControllerBase {}

public record CreateHardwareSpecRequest;

public record HardwareSpecResponse;