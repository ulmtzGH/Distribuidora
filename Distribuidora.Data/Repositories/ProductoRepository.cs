using Distribuidora.Data.Base;
using Distribuidora.Data.Configuration;
using Distribuidora.Data.Extensions;
using Distribuidora.Data.Interfaces;
using Distribuidora.Entities.DTOs;
using Distribuidora.Entities.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Distribuidora.Data.Repositories
{
    public class ProductoRepository : RepositoryBase, IProductoRepository
    {

        public ProductoRepository(SqlConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task ActualizarAsync(ProductoEdicionDTO producto)
        {
            using var connection = await OpenConnectionAsync();

            using SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using (SqlCommand command = new("spProductoActualizar", connection, transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                    command.Parameters.AddWithValue("@Clave", producto.Clave);
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@IdTipoProducto", producto.IdTipoProducto);
                    command.Parameters.AddWithValue("@Precio", (object?)producto.Precio ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Activo", producto.Activo);

                    await command.ExecuteNonQueryAsync();
                }

                using (SqlCommand command = new("spProductoProveedorEliminarPorProducto", connection, transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdProducto", producto.IdProducto);

                    await command.ExecuteNonQueryAsync();
                }

                foreach (var proveedor in producto.Proveedores)
                {
                    using SqlCommand command = new("spProductoProveedorInsertar", connection, transaction);

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                    command.Parameters.AddWithValue("@IdProveedor", proveedor.IdProveedor);
                    command.Parameters.AddWithValue("@ClaveProveedor", proveedor.ClaveProveedor);
                    command.Parameters.AddWithValue("@Costo", proveedor.Costo);

                    await command.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<ProductoBusquedaDTO>> BuscarAsync(string? clave, int? idTipoProducto)
        {
            using var connection = await OpenConnectionAsync();

            using var command = CreateStoredProcedure(connection, "spProductoBuscar");

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Clave", string.IsNullOrWhiteSpace(clave) ? DBNull.Value : clave);
            command.Parameters.AddWithValue("@IdTipoProducto", idTipoProducto.HasValue ? idTipoProducto: DBNull.Value);

            //command.AddNullableString("@Clave", clave, 50);
            //command.AddNullableInt("@IdTipoProducto", idTipoProducto);

            using var reader = await command.ExecuteReaderAsync();

            var productos = new List<ProductoBusquedaDTO>();

            while (await reader.ReadAsync())
            {
                productos.Add(new ProductoBusquedaDTO
                {
                    IdProducto = reader.GetInt32("IdProducto"),
                    Clave = reader.GetString("Clave"),
                    Nombre = reader.GetString("Nombre"),
                    TipoProducto = reader.GetString("TipoProducto"),
                    Precio = reader.GetNullableDecimal("Precio"),
                    Activo = reader.GetBoolean("Activo")
                });
            }

            return productos;
        }

        public async Task EliminarAsync(int idProducto)
        {
            using var connection = await OpenConnectionAsync();

            using SqlCommand command = new("spProductoEliminar", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdProducto", idProducto);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<int> InsertarAsync(ProductoEdicionDTO producto)
        {
            using var connection = await OpenConnectionAsync();

            using SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                int idProducto;

                using (SqlCommand command = new("spProductoInsertar", connection, transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Clave", producto.Clave);
                    command.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    command.Parameters.AddWithValue("@IdTipoProducto", producto.IdTipoProducto);
                    command.Parameters.AddWithValue("@Precio", (object?)producto.Precio ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Activo", producto.Activo);

                    idProducto = Convert.ToInt32(await command.ExecuteScalarAsync());
                }

                foreach (var proveedor in producto.Proveedores)
                {
                    using SqlCommand command = new("spProductoProveedorInsertar", connection, transaction);

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdProducto", idProducto);
                    command.Parameters.AddWithValue("@IdProveedor", proveedor.IdProveedor);
                    command.Parameters.AddWithValue("@ClaveProveedor", proveedor.ClaveProveedor);
                    command.Parameters.AddWithValue("@Costo", proveedor.Costo);

                    await command.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();

                return idProducto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ProductoEdicionDTO?> ObtenerAsync(int idProducto)
        {
            using var connection = await OpenConnectionAsync();

            using SqlCommand command = new("spProductoObtener", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdProducto", idProducto);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            ProductoEdicionDTO? producto = null;

            if (await reader.ReadAsync())
            {
                producto = new ProductoEdicionDTO
                {
                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                    Clave = reader.GetString(reader.GetOrdinal("Clave")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    IdTipoProducto = reader.GetInt32(reader.GetOrdinal("IdTipoProducto")),
                    TipoProducto = reader.GetString(reader.GetOrdinal("TipoProducto")),
                    Precio = reader.IsDBNull(reader.GetOrdinal("Precio"))
                                ? null
                                : reader.GetDecimal(reader.GetOrdinal("Precio")),
                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                };
            }

            if (producto == null)
                return null;

            if (await reader.NextResultAsync())
            {
                while (await reader.ReadAsync())
                {
                    producto.Proveedores.Add(new ProductoProveedorDTO
                    {
                        IdProductoProveedor = reader.GetInt32(reader.GetOrdinal("IdProductoProveedor")),
                        IdProveedor = reader.GetInt32(reader.GetOrdinal("IdProveedor")),
                        NombreProveedor = reader.GetString(reader.GetOrdinal("NombreProveedor")),
                        ClaveProveedor = reader.GetString(reader.GetOrdinal("ClaveProveedor")),
                        Costo = reader.GetDecimal(reader.GetOrdinal("Costo"))
                    });
                }
            }

            return producto;
        }
    }
}
