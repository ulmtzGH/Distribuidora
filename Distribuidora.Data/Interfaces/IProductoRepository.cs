using Distribuidora.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribuidora.Data.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<ProductoBusquedaDTO>> BuscarAsync(string? clave, int? idTipoProducto);

        Task<ProductoEdicionDTO?> ObtenerAsync(int idProducto);

        Task<int> InsertarAsync(ProductoEdicionDTO producto);

        Task ActualizarAsync(ProductoEdicionDTO producto);

        Task EliminarAsync(int idProducto);
    }
}
