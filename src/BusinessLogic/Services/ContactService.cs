using BusinessLogic.Domain;

namespace BusinessLogic.Services;

public class ContactService
{
    private static readonly List<Contact> ContactRepository = [];

    public void Create(Contact contact)
    {
        // store contact in database
        ContactRepository.Add(contact);
    }

    public Contact? Get(Guid id)
    {
        // find contact in database
        return ContactRepository.Find(x => x.Id == id);
    }
}