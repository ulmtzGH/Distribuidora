using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribuidora.Entities.DTOs
{
    public class ProductoEdicionDTO
    {
        public int IdProducto { get; set; }

        public string Clave { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string? TipoProducto { get; set; } = string.Empty;

        public int IdTipoProducto { get; set; }

        public decimal? Precio { get; set; }

        public bool Activo { get; set; }

        public List<ProductoProveedorDTO> Proveedores { get; set; } = new();
    }
}
