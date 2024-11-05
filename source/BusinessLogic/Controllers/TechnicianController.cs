using source.BusinessLogic.Domain;
using source.BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;

namespace source.BusinessLogic.Controllers;

[ApiController]
[Route("api/technician")]
public class TechnicianController(TechnicianService service) : ControllerBase
{
    private readonly TechnicianService _service = service;

    [HttpPost]
    public IActionResult Create(CreateTechnicianRequest request)
    {
        // mapping to internal representation
        var technician = request.ToDomain();

        // invoking the use case
        //_service.Create(technician);

        // mapping to external representation
        return CreatedAtAction(
            actionName: nameof(this.Get),
            routeValues: new { technician.Id },
            value: TechnicianResponse.FromDomain(technician)
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        // invoking the use case
        Technician? technician = await _service.Get(id);

        // return status 200 ok response
        return technician is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "Technician not found")
            : Ok(TechnicianResponse.FromDomain(technician));
    }
}

public record CreateTechnicianRequest(
    TechnicianRole Role,
    string Name,
    string Surname,
    decimal Salary
)
{
    public Technician ToDomain()
    {
        return new Technician
        {
            Role = Role,
            Name = Name,
            Surname = Surname,
            Salary = Salary,
        };
    }
}

public record TechnicianResponse(
    Guid Id,
    TechnicianState State,
    TechnicianRole Role,
    string Name,
    string Surname,
    DateTime Hired,
    decimal Salary
)
{
    public static TechnicianResponse FromDomain(Technician technician)
    {
        return new TechnicianResponse(
            technician.Id,
            technician.State,
            technician.Role,
            technician.Name,
            technician.Surname,
            technician.Hired,
            technician.Salary
        );
    }
}