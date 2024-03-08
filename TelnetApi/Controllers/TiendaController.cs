using DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelnetApi.Dto;

namespace TelnetApi.Controllers
{
    [Route("servicios/[controller]")]
    [ApiController]
    public class TiendaController : Controller
    {
        private TelnetDbContext _dbContext;

        public TiendaController(TelnetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult ObtenerTiendas()
        {
            var tiendas = _dbContext.Tienda.ToList();
            var result = new
            {
                tiendas = tiendas.Select(tienda => new
                {
                    tienda.id,
                    tienda.nombre,
                    tienda.direccion
                })
            };
            return Ok(result);
        }

        [HttpPost("/servicios/tienda/creartienda")]
        public ActionResult CrearTienda([FromBody] DtoTienda tienda)
        {
            var newTienda = new Tienda
            {
                nombre = tienda.nombre,
                direccion = tienda.direccion
            };
            _dbContext.Tienda.Add(newTienda);
            _dbContext.SaveChanges();

            var result = new
            {
                tienda = newTienda
            };
            return CreatedAtAction(nameof(ObtenerTiendas),result);
        }

        [HttpDelete("/servicios/tienda/eliminartienda/tiendaId")]
        public ActionResult EliminarTienda(string tiendaId)
        {
            var parsedTiendaID = int.Parse(tiendaId);
            var existeArticulo = _dbContext.Tienda.Find(parsedTiendaID);
            if (existeArticulo != null)
            {
                _dbContext.Remove(existeArticulo);
                _dbContext.SaveChanges();
                var result = new
                {
                    succes = true
                };
                return Ok(result);
            }
            else
            {
                var result = new
                {
                    succes = false
                };
                return Ok(result);
            }
            
        }

        
    }
}
