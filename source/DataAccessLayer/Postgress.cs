using System.Data;
using Npgsql;
using source.ConfigurationLayer;

namespace source.DataAccessLayer
{
    public class Postgress : IDisposable
    {
        private readonly string _connectionString = EnvironmentVariable.POSTGRES_CONNECTION_STRING;
        private NpgsqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the Postgress class.
        /// </summary>
        public Postgress()
        {
            _connection = new NpgsqlConnection(_connectionString);
            Open();
            ExecuteNonQuery(SqlQueries.TryCreateTables);
        }

        /// <summary>
        /// Opens the database connection if it's not already open.
        /// </summary>
        public void Open()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        /// <summary>
        /// Closes the database connection if it's not already closed.
        /// </summary>
        public void Close()
        {
            if (_connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// Returns the current database connection.
        /// </summary>
        /// <returns>The NpgsqlConnection object.</returns>
        public NpgsqlConnection Connection()
        {
            return _connection;
        }

        /// <summary>
        /// Executes a non-query command against the database.
        /// </summary>
        /// <param name="query">The SQL command to execute.</param>
        /// <param name="parameters">The parameters for the SQL command.</param>
        public void ExecuteNonQuery(string query, params NpgsqlParameter[] parameters)
        {
            Open();

            using (var command = new NpgsqlCommand(query, _connection))
            {
                command.Parameters.AddRange(parameters);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Database command failed: " + ex);
                }
            }
        }

        /// <summary>
        /// Executes a query command against the database and returns a data reader.
        /// </summary>
        /// <param name="query">The SQL command to execute.</param>
        /// <param name="parameters">The parameters for the SQL command.</param>
        /// <returns>A NpgsqlDataReader object.</returns>
        public NpgsqlDataReader ExecuteQuery(string query, params NpgsqlParameter[] parameters)
        {
            Open();

            var command = new NpgsqlCommand(query, _connection);
            command.Parameters.AddRange(parameters);

            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Executes an asynchronous query command against the database and returns a data reader.
        /// </summary>
        /// <param name="query">The SQL command to execute.</param>
        /// <param name="parameters">The parameters for the SQL command.</param>
        /// <returns>A Task containing a NpgsqlDataReader object.</returns>
        public async Task<NpgsqlDataReader> ExecuteQueryAsync(string query, params NpgsqlParameter[] parameters)
        {
            Open();

            var command = new NpgsqlCommand(query, _connection);
            command.Parameters.AddRange(parameters);

            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Closes the database connection and disposes of the resources.
        /// </summary>
        public void Dispose()
        {
            Close();
            _connection?.Dispose();
        }
    }
}
