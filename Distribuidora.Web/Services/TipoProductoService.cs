using Distribuidora.Web.Services.Interfaces;
using Distribuidora.Web.ViewModels;

namespace Distribuidora.Web.Services
{
    public class TipoProductoService : ITipoProductoService
    {
        private readonly HttpClient _httpClient;

        public TipoProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TipoProductoViewModel>> ObtenerTodosAsync()
        {
            var tipos = await _httpClient.GetFromJsonAsync<List<TipoProductoViewModel>>(
                "api/TipoProducto");

            return tipos ?? new List<TipoProductoViewModel>();
        }
    }
}
