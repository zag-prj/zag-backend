using System.Data;
using System.Threading.Tasks;
using source.BusinessLogic.Domain;
using source.DataAccessLayer;

namespace source.BusinessLogic.Services
{
    public class TechnicianService
    {
        private readonly Postgress _db;

        public TechnicianService(Postgress db)
        {
            _db = db;
        }

        public async Task<Technician?> Get(Guid id)
        {
            var query = SqlQueries.GetTechnicianById;
            var parameters = new { technicianId = id };

            using (var reader = await _db.ExecuteQueryAsync(query, parameters))
            {
                if (await reader.ReadAsync())
                {
                    return new Technician
                    {
                        Id = reader.GetGuid(0),
                        State = (TechnicianState)reader.GetInt32(1),
                        Role = (TechnicianRole)reader.GetInt32(2),
                        Name = reader.GetString(3),
                        Surname = reader.GetString(4),
                        Hired = reader.GetDateTime(5),
                        Salary = reader.GetDecimal(6)
                    };
                }
            }
            return null;
        }
    }
}
