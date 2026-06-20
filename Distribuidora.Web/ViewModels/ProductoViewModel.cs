using System.ComponentModel.DataAnnotations;

namespace Distribuidora.Web.ViewModels
{
    public class ProductoViewModel
    {
        public int IdProducto { get; set; }

        [Required]
        [Display(Name = "Clave")]
        public string Clave { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        //[Required]
        //[Display(Name = "Tipo Producto")]
        //public string TipoProducto { get; set; } = string.Empty;

        public int IdTipoProducto { get; set; }

        public string? TipoProducto { get; set; }

        [Required]
        [Range(0, 999999)]
        public decimal? Precio { get; set; }

        public bool Activo { get; set; }

        public List<ProductoProveedorViewModel> Proveedores { get; set; } = new();
    }
}
