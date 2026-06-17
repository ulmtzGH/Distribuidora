using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribuidora.Entities.Models
{
    public class ProductoProveedor
    {
        public int IdProductoProveedor { get; set; }

        public int IdProducto { get; set; }

        public int IdProveedor { get; set; }

        public string ClaveProveedor { get; set; } = string.Empty;

        public decimal Costo { get; set; }
    }
}
