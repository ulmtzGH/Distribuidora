namespace Distribuidora.Web.ViewModels
{
    public class ProductoProveedorViewModel
    {
        public int IdProveedor { get; set; }

        public string NombreProveedor { get; set; } = string.Empty;

        public string ClaveProveedor { get; set; } = string.Empty;

        public decimal Costo { get; set; }
    }
}
