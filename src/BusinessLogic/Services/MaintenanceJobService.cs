using BusinessLogic.Domain;

namespace BusinessLogic.Services;

public class MaintenanceJobService
{
    private static readonly List<MaintenanceJob> Repository = [];

    public void Create(MaintenanceJob job)
    {
        // store job in database
        Repository.Add(job);
    }

    public MaintenanceJob? Get(Guid clientId, Guid technicianId)
    {
        // find job in database
        return Repository.Find(x => (x.ClientId == clientId) && (x.TechnicianId == technicianId));
    }
}