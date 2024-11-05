using source.BusinessLogic.Domain;
using source.DataAccessLayer;
using Npgsql;

namespace source.BusinessLogic.Services
{
    public class ContactService
    {
        private readonly Postgress _postgress;

        public ContactService(Postgress postgress)
        {
            _postgress = postgress;
        }

        public async Task<Contact?> Get(Guid id)
        {
            var query = SqlQueries.GetContactById;
            var parameters = new[] { new NpgsqlParameter("@contactId", id) };

            // Executing the query
            using (var reader = await _postgress.ExecuteQueryAsync(query, parameters))
            {
                if (await reader.ReadAsync())
                {
                    return new Contact
                    {
                        Id = reader.GetGuid(0),
                        ClientId = reader.GetGuid(1),
                        Name = reader.GetString(2),
                        Surname = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Email = reader.GetString(5)
                    };
                }
                return null;
            }
        }
    }
}
