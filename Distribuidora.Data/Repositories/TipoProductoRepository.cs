using Distribuidora.Data.Base;
using Distribuidora.Data.Configuration;
using Distribuidora.Data.Interfaces;
using Distribuidora.Entities.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribuidora.Data.Repositories
{
    public class TipoProductoRepository : RepositoryBase, ITipoProductoRepository
    {
        public TipoProductoRepository(SqlConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<List<TipoProducto>> ListarAsync()
        {
            List<TipoProducto> tipos = new();

            using var connection = await OpenConnectionAsync();

            using SqlCommand command = new("spTipoProductoListar", connection);

            command.CommandType = CommandType.StoredProcedure;

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                tipos.Add(new TipoProducto
                {
                    IdTipoProducto = reader.GetInt32(reader.GetOrdinal("IdTipoProducto")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                });
            }

            return tipos;
        }
    }
}
