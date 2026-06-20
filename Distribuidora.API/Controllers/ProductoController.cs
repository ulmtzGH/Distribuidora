using Distribuidora.Data.Interfaces;
using Distribuidora.Entities.DTOs;
using Distribuidora.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Distribuidora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(
            [FromQuery] string? clave,
            [FromQuery] int? idTipoProducto)
        {
            var productos = await _productoRepository.BuscarAsync(
                clave,
                idTipoProducto);

            return Ok(productos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var producto = await _productoRepository.ObtenerAsync(id);

            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar(
            [FromBody] ProductoEdicionDTO producto)
        {
            int id = await _productoRepository.InsertarAsync(producto);

            producto.IdProducto = id;

            return CreatedAtAction(
                nameof(Obtener),
                new { id },
                producto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(
            int id,
            [FromBody] ProductoEdicionDTO producto)
        {
            if (id != producto.IdProducto)
                return BadRequest();

            await _productoRepository.ActualizarAsync(producto);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _productoRepository.EliminarAsync(id);

            return NoContent();
        }
    }
}
