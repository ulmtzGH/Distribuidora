using Distribuidora.Data.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Distribuidora.Data.Base
{
    public abstract class RepositoryBase
    {
        private readonly SqlConnectionFactory _connectionFactory;

        protected RepositoryBase(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        protected SqlConnection CreateConnection()
        {
            return _connectionFactory.CreateConnection();
        }

        protected async Task<SqlConnection> OpenConnectionAsync()
        {
            var connection = CreateConnection();

            await connection.OpenAsync();

            return connection;
        }

        protected SqlCommand CreateStoredProcedure(
            SqlConnection connection,
            string procedureName)
        {
            return new SqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
        }
    }
}