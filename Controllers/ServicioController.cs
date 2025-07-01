using CoreSistemaMasajes.Data;
using CoreSistemaMasajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreSistemaMasajes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ServicioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/servicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> GetServicios()
        {
            return await _context.Servicios
                .FromSqlRaw("EXEC sp_ObtenerServicios")
                .ToListAsync();
        }

        // POST: api/servicio
        [HttpPost]
        public async Task<IActionResult> PostServicio(Servicio servicio)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC sp_InsertarServicio 
                    @NombreServicio = {servicio.NombreServicio},
                    @PrecioServicio = {servicio.PrecioServicio},
                    @DuracionPromedioMinutos = {servicio.DuracionPromedioMinutos}");

            return Ok("Servicio insertado correctamente");
        }

        // PUT: api/servicio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServicio(int id, Servicio servicio)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC sp_ActualizarServicio 
                    @ServicioId = {id},
                    @NombreServicio = {servicio.NombreServicio},
                    @PrecioServicio = {servicio.PrecioServicio},
                    @DuracionPromedioMinutos = {servicio.DuracionPromedioMinutos}");

            return Ok("Servicio actualizado");
        }

        // DELETE: api/servicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC sp_EliminarServicio @ServicioId = {id}");

            return Ok("Servicio eliminado lógicamente");
        }
    }
}

