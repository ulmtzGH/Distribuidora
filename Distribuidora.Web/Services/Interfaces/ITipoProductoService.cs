using Distribuidora.Web.ViewModels;

namespace Distribuidora.Web.Services.Interfaces
{
    public interface ITipoProductoService
    {
        Task<List<TipoProductoViewModel>> ObtenerTodosAsync();
    }
}
