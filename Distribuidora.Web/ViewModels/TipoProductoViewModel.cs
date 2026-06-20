namespace Distribuidora.Web.ViewModels
{
    public class TipoProductoViewModel
    {
        public int IdTipoProducto { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }
    }
}
