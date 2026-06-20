using Distribuidora.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Distribuidora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoProductoController : Controller
    {
        //private readonly ITipoProductoService _service;

        //public TipoProductoController(ITipoProductoService service)
        //{
        //    _service = service;
        //}

        private readonly ITipoProductoRepository _tipoProductoRepository;

        public TipoProductoController(ITipoProductoRepository tipoProductoRepository)
        {
            _tipoProductoRepository = tipoProductoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var tipos = await _tipoProductoRepository.ListarAsync();

            return Ok(tipos);
        }
    }
}
