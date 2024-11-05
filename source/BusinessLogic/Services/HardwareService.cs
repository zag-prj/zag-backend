using source.BusinessLogic.Domain;
using source.DataAccessLayer;

using System.Data;
using System.Threading.Tasks;

namespace source.BusinessLogic.Services
{
    public class HardwareService
    {
        private readonly Postgress _db;

        public HardwareService(Postgress db)
        {
            _db = db;
        }

        public async Task<Hardware?> Get(Guid id)
        {
            var query = SqlQueries.GetHardwareById;
            var parameters = new Npgsql.NpgsqlParameter[]
            {
                new Npgsql.NpgsqlParameter("@hardwareId", id)
            };

            using (var reader = await _db.ExecuteQueryAsync(query, parameters))
            {
                if (await reader.ReadAsync())
                {
                    return new Hardware
                    {
                        Id = reader.GetGuid(0),
                        State = (HardwareState)reader.GetInt32(1),
                        ContractId = reader.GetGuid(2),
                        RefName = reader.GetString(3),
                        Value = reader.GetDecimal(4)
                    };
                }
            }
            return null;
        }
    }

    public class SpecService
    {
        private readonly Postgress _db;

        public SpecService(Postgress db)
        {
            _db = db;
        }

        public async Task<Spec?> Get(Guid id)
        {
            var query = SqlQueries.GetSpecById;
            //var parameters = new { specId = id };
            var parameters = new Npgsql.NpgsqlParameter[]
            {
                new Npgsql.NpgsqlParameter("@specId", id)
            };

            using (var reader = await _db.ExecuteQueryAsync(query, parameters))
            {
                if (await reader.ReadAsync())
                {
                    return new Spec
                    {
                        Id = reader.GetGuid(0),
                        RefName = reader.GetString(1),
                        Value = reader.GetDecimal(2)
                    };
                }
            }
            return null;
        }
    }

    public class HardwareSpecService
    {
        private readonly Postgress _db;

        public HardwareSpecService(Postgress db)
        {
            _db = db;
        }

        public async Task<HardwareSpec?> Get(Guid hardwareId, Guid specId)
        {
            var query = SqlQueries.GetHardwareSpec;
            //var parameters = new { hardwareId, specId };

            var parameters = new Npgsql.NpgsqlParameter[]
            {
                new Npgsql.NpgsqlParameter("@hardwareId", hardwareId),
                new Npgsql.NpgsqlParameter("@specId", specId)
            };

            using (var reader = await _db.ExecuteQueryAsync(query, parameters))
            {
                if (await reader.ReadAsync())
                {
                    return new HardwareSpec
                    {
                        HardwareId = reader.GetGuid(0),
                        SpecId = reader.GetGuid(1),
                        Count = reader.GetInt32(2)
                    };
                }
            }
            return null;
        }
    }
}

