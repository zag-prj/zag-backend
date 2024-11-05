using source.BusinessLogic.Domain;
using source.BusinessLogic.Services;

using Microsoft.AspNetCore.Mvc;

namespace source.BusinessLogic.Controllers;

[ApiController]
[Route("api/job")]
public class MaintenanceJobController(MaintenanceJobService service) : ControllerBase
{
    private readonly MaintenanceJobService _service = service;

    [HttpPost]
    public IActionResult Create(CreateMaintenanceJobRequest request)
    {
        // mapping to internal representation
        var job = request.ToDomain();

        // invoking the use case
        _service.Create(job);

        // mapping to external representation
        return CreatedAtAction(
            actionName: nameof(this.Get),
            routeValues: new
            {
                job.ClientId,
                job.TechnicianId,
            },
            value: MaintenanceJobResponse.FromDomain(job)
        );
    }

    [HttpGet("{clientId:guid}/{technicianId:guid}")]
    public IActionResult Get(Guid clientId, Guid technicianId)
    {
        // invoking the use case
        MaintenanceJob? job = _service.Get(clientId, technicianId);

        // return 200 ok response
        return job is null
        ? Problem(statusCode: StatusCodes.Status404NotFound, detail: "MaintenanceJob not found")
        : Ok(MaintenanceJobResponse.FromDomain(job));
    }
}

public record CreateMaintenanceJobRequest(
    Guid ClientId,
    Guid TechnicianId
)
{
    public MaintenanceJob ToDomain()
    {
        return new MaintenanceJob
        {
            ClientId = ClientId,
            TechnicianId = TechnicianId,
        };
    }
}

public record MaintenanceJobResponse(
    Guid ClientId,
    Guid TechnicianId,
    MaintenanceJobState State,
    DateTime Requested,

    DateTime? Started,
    DateTime? Completed
)
{
    public static MaintenanceJobResponse FromDomain(MaintenanceJob job)
    {
        return new MaintenanceJobResponse(
            job.ClientId,
            job.TechnicianId,
            job.State,
            job.Requested,
            job.Started,
            job.Completed
        );
    }
}