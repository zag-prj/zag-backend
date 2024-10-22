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
public class SpecController(SpecService service) : ControllerBase
{
    private readonly SpecService _service = service;

    public IActionResult Create(CreateSpecRequest request)
    {
        // mapping to internal representation
        var spec = request.ToDomain();

        // invoking the use case
        _service.Create(spec);

        // mapping to external representation
        return CreatedAtAction(
            actionName: nameof(this.Get),
            routeValues: new { spec.Id },
            value: SpecResponse.FromDomain(spec)
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        // invoking the use case
        Spec? spec = _service.Get(id);

        // return 200 ok response
        return spec is null
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Spec not found")
        : Ok(SpecResponse.FromDomain(spec));
    }
}

public record CreateSpecRequest(
    string RefName,
    decimal Value
)
{
    public Spec ToDomain()
    {
        return new Spec
        {
            RefName = RefName,
            Value = Value,
        };
    }
}

public record SpecResponse(
    Guid Id,
    string RefName,
    decimal Value
)
{
    public static SpecResponse FromDomain(Spec spec)
    {
        return new SpecResponse(
            spec.Id,
            spec.RefName,
            spec.Value
        );
    }
}

// TODO: decide on endpoint path
[ApiController]
[Route("api/hardware/info")]
public class HardwareSpecController(HardwareSpecService service) : ControllerBase
{
    private readonly HardwareSpecService _service = service;

    [HttpPost]
    public IActionResult Create(CreateHardwareSpecRequest request)
    {
        // mapping to internal representation
        var hSpec = request.ToDomain();

        // invoking the use case
        _service.Create(hSpec);

        // mapping to external representation
        return CreatedAtAction(
            actionName: nameof(this.Get),
            routeValues: new { hSpec.HardwareId, hSpec.SpecId },
            value: HardwareSpecResponse.FromDomain(hSpec)
        );
    }

    [HttpGet("{hardwareId:guid}/{specId:guid}")]
    public IActionResult Get(Guid hardwareId, Guid specId)
    {
        // invoking the use case
        HardwareSpec? hSpec = _service.Get(hardwareId, specId);

        return hSpec is null
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "HardwareSpec not found")
        : Ok(HardwareSpecResponse.FromDomain(hSpec));
    }
}

public record CreateHardwareSpecRequest(
    Guid HardwareId,
    Guid SpecId,
    int Count
)
{
    public HardwareSpec ToDomain()
    {
        return new HardwareSpec
        {
            HardwareId = HardwareId,
            SpecId = SpecId,
            Count = Count,
        };
    }
}

public record HardwareSpecResponse(
    Guid HardwareId,
    Guid SpecId,
    int Count
)
{
    public static HardwareSpecResponse FromDomain(HardwareSpec hSpec)
    {
        return new HardwareSpecResponse(
            hSpec.HardwareId,
            hSpec.SpecId,
            hSpec.Count
        );
    }
}