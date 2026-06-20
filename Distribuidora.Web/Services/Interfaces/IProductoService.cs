using Distribuidora.Web.ViewModels;

namespace Distribuidora.Web.Services.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoViewModel>> ObtenerProductosAsync();
        Task CrearAsync(ProductoViewModel producto);
    }
}
