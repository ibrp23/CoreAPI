using CoreSistemaMasajes.Data;
using CoreSistemaMasajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CoreSistemaMasajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialSistemaController: ControllerBase
    {
        private readonly AppDbContext _context;

        public HistorialSistemaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialSistema>>> GetHistorial()
        {
            return await _context.HistorialSistema
                .Include(h => h.Usuario)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<HistorialSistema>> PostHistorial(HistorialSistema historial)
        {
            historial.FechaHora = DateTime.Now;

            _context.HistorialSistema.Add(historial);
            await _context.SaveChangesAsync();

            return CreatedAtAction(null, new { id = historial.Id }, historial);
        }
    }
}
