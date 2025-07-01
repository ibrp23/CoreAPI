using CoreSistemaMasajes.Data;
using CoreSistemaMasajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreSistemaMasajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CitaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CitaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cita
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitas()
        {
            return await _context.Citas
                .FromSqlRaw("EXEC sp_ObtenerCitas")
                .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCita(int id)
        {
            var citas = await _context.Citas
                .FromSqlRaw("EXEC sp_ObtenerCitaPorId @Id = {0}", id)
                .ToListAsync();

            var cita = citas.FirstOrDefault();

            if (cita == null)
                return NotFound();

            return cita;
        }

        // POST: api/cita
        [HttpPost]
        public async Task<IActionResult> PostCita(Cita cita)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC sp_InsertarCita 
                    @ClienteId = {cita.ClienteId},
                    @ServicioId = {cita.ServicioId},
                    @EmpleadoId = {cita.EmpleadoId},
                    @FechaHoraCita = {cita.FechaHoraCita},
                    @FechaHoraIngresado = {cita.FechaHoraIngresado},
                    @Estado = {cita.Estado},
                    @Observaciones = {cita.Observaciones}");

            return Ok("Cita insertada correctamente");
        }

        // PUT: api/cita/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, Cita cita)
        {
            if (id != cita.CitaId)
                return BadRequest("El ID no coincide");

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC sp_ActualizarCita 
                    @CitaId = {id},
                    @ClienteId = {cita.ClienteId},
                    @ServicioId = {cita.ServicioId},
                    @EmpleadoId = {cita.EmpleadoId},
                    @FechaHoraCita = {cita.FechaHoraCita},
                    @FechaHoraIngresado = {cita.FechaHoraIngresado},
                    @Estado = {cita.Estado},
                    @Observaciones = {cita.Observaciones}");

            return Ok("Cita actualizada correctamente");
        }

        // DELETE: api/cita/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC sp_EliminarCita @CitaId = {id}");

            return Ok("Cita eliminada correctamente");
        }
    }
}
