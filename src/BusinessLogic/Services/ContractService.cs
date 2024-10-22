using BusinessLogic.Domain;

namespace BusinessLogic.Services;

public class ContractService
{
    private static readonly List<Contract> ContractRepository = [];

    public void Create(Contract contract)
    {
        // store contract in database
        ContractRepository.Add(contract);
    }

    public Contract? Get(Guid id)
    {
        // find contract in database
        return ContractRepository.Find(x => x.Id == id);
    }
}