using Distribuidora.Web.Services;
using Distribuidora.Web.Services.Interfaces;
using Distribuidora.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;

namespace Distribuidora.Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly ITipoProductoService _tipoProductoService;


        public ProductoController(IProductoService productoService, ITipoProductoService tipoProductoService)
        {
            _productoService = productoService;
            _tipoProductoService = tipoProductoService;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.ObtenerProductosAsync();

            return View(productos);
        }

        public async Task<IActionResult> Crear()
        {
            //return View("Editar", new ProductoViewModel());
            //var tipos = await _tipoProductoRepository.ListarAsync();
            var tipos = await _tipoProductoService.ObtenerTodosAsync();

            ViewBag.TiposProducto = new SelectList(
                tipos,
                "IdTipoProducto",
                "Nombre");

            return View("Editar", new ProductoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProductoViewModel model)
        {
            //if (!ModelState.IsValid)
            //    return View("Editar", model);

            ////var response = await _httpClient.PostAsJsonAsync(
            ////    "api/Producto",
            ////    model);

            ////if (response.IsSuccessStatusCode)
            ////    return RedirectToAction(nameof(Index));

            ////ModelState.AddModelError("", "No fue posible guardar el producto.");

            ////return View("Editar", model);

            //await _productoService.CrearAsync(model);
            //return RedirectToAction(nameof(Index));

            if (!ModelState.IsValid)
            {
                var tipos = await _tipoProductoService.ObtenerTodosAsync();

                ViewBag.TiposProducto = new SelectList(
                    tipos,
                    "IdTipoProducto",
                    "Nombre");

                return View("Editar", model);
            }

            await _productoService.CrearAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
