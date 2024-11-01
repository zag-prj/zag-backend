using BusinessLogic.Domain;
using source.DataAccessLayer;
using Npgsql;

namespace BusinessLogic.Services
{
    public class ContractService
    {
        private readonly Postgress _postgress;

        public ContractService(Postgress postgress)
        {
            _postgress = postgress;
        }

        public async Task<Contract?> Get(Guid id)
        {
            var query = SqlQueries.GetContractById;
            var parameters = new[] { new NpgsqlParameter("@contractId", id) };

            // Executing the query
            using (var reader = await _postgress.ExecuteQueryAsync(query, parameters))
            {
                if (await reader.ReadAsync())
                {
                    return new Contract
                    {
                        Id = reader.GetGuid(0),
                        State = (ContractState)reader.GetInt32(1),
                        ClientId = reader.GetGuid(2),
                        Issued = reader.GetDateTime(3),
                        Until = reader.GetDateTime(4),
                        PriceMonthly = reader.GetDecimal(5)
                    };
                }
                return null;
            }
        }
    }
}
