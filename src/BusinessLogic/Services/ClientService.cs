using BusinessLogic.Domain;
using source.DataAccessLayer;
using System;
using System.Threading.Tasks;
using Npgsql;

namespace BusinessLogic.Services;

public class ClientService
{
    private readonly Postgress _postgress;

    public ClientService(Postgress postgress)
    {
        _postgress = postgress;
    }

    public async Task<Client?> Get(Guid id)
    {
        //Query parameters
        var query = SqlQueries.GetClientById;
        var parameters = new[] { new NpgsqlParameter("@clientid", id) };

        //Executing the query
        using (var reader = await _database.ExecuteQueryAsync(SqlQueries.GetClientById, parameters))
        {
            if (await reader.ReadAsync())
            {
                return new Client
                {
                    Id = reader.GetGuid(0),
                    State = reader.GetString(1),
                    CompanyName = reader.GetString(2),
                    Address = reader.GetString(3),
                    Billing = reader.GetString(4),
                    Password = reader.GetString(5)
                };               
            }
            return null;

        }
    }
}
