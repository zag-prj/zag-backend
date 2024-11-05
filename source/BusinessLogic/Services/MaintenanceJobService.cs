using source.BusinessLogic.Domain;
using source.DataAccessLayer;

namespace source.BusinessLogic.Services
{
    public class MaintenanceJobService
    {
        private readonly Postgress _db;

        public MaintenanceJobService(Postgress db)
        {
            _db = db;
        }

        public async Task<MaintenanceJob?> Get(Guid clientId, Guid technicianId)
        {
            var query = SqlQueries.GetMaintenanceJob;
            var parameters = new Npgsql.NpgsqlParameter[]
            {
                new Npgsql.NpgsqlParameter("@clientId", clientId),
                new Npgsql.NpgsqlParameter("@technicianId", technicianId)
            };

            using (var reader = await _db.ExecuteQueryAsync(query, parameters))
            {
                if (await reader.ReadAsync())
                {
                    return new MaintenanceJob
                    {
                        ClientId = reader.GetGuid(0),
                        TechnicianId = reader.GetGuid(1),
                        State = (MaintenanceJobState)reader.GetInt32(2),
                        Requested = reader.GetDateTime(3),
                        Started = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                        Completed = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                    };
                }
            }
            return null;
        }
    }
}