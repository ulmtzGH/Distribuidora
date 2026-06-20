using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribuidora.Entities.DTOs
{
    public class ProductoProveedorDTO
    {
        public int IdProductoProveedor { get; set; }
        public int IdProveedor { get; set; }

        public string NombreProveedor { get; set; } = string.Empty;

        public string ClaveProveedor { get; set; } = string.Empty;

        public decimal Costo { get; set; }
    }
}
