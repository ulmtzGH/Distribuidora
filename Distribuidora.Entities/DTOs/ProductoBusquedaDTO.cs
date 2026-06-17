using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribuidora.Entities.DTOs
{
    public class ProductoBusquedaDTO
    {
        public int IdProducto { get; set; }

        public string Clave { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string TipoProducto { get; set; } = string.Empty;

        public decimal? Precio { get; set; }

        public bool EsActivo { get; set; }
    }
}
