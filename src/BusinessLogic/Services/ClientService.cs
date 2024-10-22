using BusinessLogic.Domain;

namespace BusinessLogic.Services;

public class ClientService
{
    private static readonly List<Client> Repository = [];

    public void Create(Client client)
    {
        // store client in database
        Repository.Add(client);
    }

    public Client? Get(Guid id)
    {
        // find client in database
        return Repository.Find(x => x.Id == id);
    }
}