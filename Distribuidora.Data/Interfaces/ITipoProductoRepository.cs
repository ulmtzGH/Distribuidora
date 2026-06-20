using Distribuidora.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribuidora.Data.Interfaces
{
    public interface ITipoProductoRepository
    {
        Task<List<TipoProducto>> ListarAsync();
    }
}
