using BusinessLogic.Domain;

namespace BusinessLogic.Services;

public class HardwareService
{
    private static readonly List<Hardware> Repository = [];

    public void Create(Hardware hardware)
    {
        // store hardware in database
        Repository.Add(hardware);
    }

    public Hardware? Get(Guid id)
    {
        // find hardware in database
        return Repository.Find(x => x.Id == id);
    }
}

public class SpecService
{
    private static readonly List<Spec> Repository = [];

    public void Create(Spec spec)
    {
        // store spec in database
        Repository.Add(spec);
    }

    public Spec? Get(Guid id)
    {
        // find spec in database
        return Repository.Find(x => x.Id == id);
    }
}

public class HardwareSpecService {
    private static readonly List<HardwareSpec> Repository = [];

    public void Create(HardwareSpec hSpec) {
        // store hardware spec in database
        Repository.Add(hSpec);
    }

    public HardwareSpec? Get(Guid hardwareId, Guid specId) {
        // find hardware spec in database
        return Repository.Find(x => (x.HardwareId == hardwareId) && (x.SpecId == specId));
    }
}