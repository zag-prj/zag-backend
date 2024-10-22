using BusinessLogic.Domain;

namespace BusinessLogic.Services;

public class TechnicianService
{
    private static readonly List<Technician> TechnicianRepository = [];

    public void Create(Technician technician)
    {
        // store technician in database
        TechnicianRepository.Add(technician);
    }

    public Technician? Get(Guid id)
    {
        // find technician in database
        return TechnicianRepository.Find(x => x.Id == id);
    }
}