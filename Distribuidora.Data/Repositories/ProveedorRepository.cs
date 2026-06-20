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
    public class ProveedorRepository : RepositoryBase, IProveedorRepository
    {
        public ProveedorRepository(SqlConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<List<Proveedor>> ListarAsync()
        {
            List<Proveedor> proveedores = new();

            using var connection = await OpenConnectionAsync();

            using SqlCommand command = new("spProveedorListar", connection);

            command.CommandType = CommandType.StoredProcedure;

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                proveedores.Add(new Proveedor
                {
                    IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                });
            }

            return proveedores;
        }
    }
}
