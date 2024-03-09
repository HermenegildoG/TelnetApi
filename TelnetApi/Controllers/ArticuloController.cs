using DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelnetApi.Dto;

namespace TelnetApi.Controllers
{
    [Route("servicios/[controller]")]
    [ApiController]
    public class ArticuloController : Controller
    {
        private TelnetDbContext _dbContext;

        public ArticuloController(TelnetDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{articuloId}")]
        public ActionResult ObtenerArticulo(int articuloId)
        {
            var articulo = _dbContext.Articulo.Include(a => a.tiendas).FirstOrDefault(a => a.id == articuloId);
            if (articulo == null)
            {
                return NotFound();
            }

            var result = new
            {
                articulo = articulo
            };

            return Ok(result);
        }

        [HttpPost("creararticulo")]
        public ActionResult CrearArticulo([FromBody] DtoArticulo articulo)
        {
            var tienda = _dbContext.Tienda.Find(articulo.tiendaId);
            if (tienda == null)
            {
                return NotFound("No se encontró la tienda especificada.");
            }

            var newArticulo = new Articulo
            {
                nombre = articulo.nombre,
                descripcion = articulo.descripcion,
                tiendas = tienda
            };
            _dbContext.Articulo.Add(newArticulo);
            _dbContext.SaveChanges();

            var result = new
            {
                articulo = newArticulo
            };
            return CreatedAtAction(nameof(ObtenerArticulo), new { articuloId = newArticulo.id }, result);
        }

        [HttpPut("actualizararticulo/{articuloId}")]
        public ActionResult ActualizarArticulo(int articuloId, [FromBody] DtoArticulo articulo)
        {
            var existingArticulo = _dbContext.Articulo.Include(a => a.tiendas).FirstOrDefault(a => a.id == articuloId);
            if (existingArticulo == null)
            {
                return NotFound();
            }

            existingArticulo.nombre = articulo.nombre;
            existingArticulo.descripcion = articulo.descripcion;
            existingArticulo.tiendaId = articulo.tiendaId;

            _dbContext.SaveChanges();

            var result = new
            {
                articulo = existingArticulo
            };

            return Ok(result);
        }

        [HttpDelete("eliminararticulo/{articuloId}")]
        public ActionResult EliminarArticulo(int articuloId)
        {
            var existeArticulo = _dbContext.Articulo.Find(articuloId);
            if (existeArticulo != null)
            {
                _dbContext.Remove(existeArticulo);
                _dbContext.SaveChanges();
                var result = new
                {
                    success = true
                };
                return Ok(result);
            }
            else
            {
                var result = new
                {
                    success = false
                };
                return Ok(result);
            }
        }
    }

}
