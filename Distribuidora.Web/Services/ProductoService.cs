using Distribuidora.Web.Services.Interfaces;
using Distribuidora.Web.ViewModels;
using System.Net.Http.Json;

namespace Distribuidora.Web.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductoViewModel>> ObtenerProductosAsync()
        {
            var productos = await _httpClient.GetFromJsonAsync<List<ProductoViewModel>>("api/Producto");

            return productos ?? new List<ProductoViewModel>();
        }

        public async Task CrearAsync(ProductoViewModel producto)
        {
            //var response = await _httpClient.PostAsJsonAsync(
            //    "api/Producto",
            //    producto);

            //response.EnsureSuccessStatusCode();
            var response = await _httpClient.PostAsJsonAsync("api/Producto", producto);

            var contenido = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Status: {(int)response.StatusCode}\n\n{contenido}");
            }
        }
    }
}
