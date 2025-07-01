using CoreSistemaMasajes.Data;
using CoreSistemaMasajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreSistemaMasajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CuentaPorCobrarController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CuentaPorCobrarController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaPorCobrar>>> GetCuentas()
        {
            return await _context.CuentasPorCobrar
                .FromSqlRaw("EXEC sp_ObtenerCuentas")
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PostCuentaPorCobrar(CuentaPorCobrar cpc)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
        EXEC sp_InsertarCuentaPorCobrar 
            @FacturaId={cpc.FacturaId},
            @MontoPendiente={cpc.MontoPendiente},
            @Pagado={cpc.Pagado}");

            return Ok("Cuenta por cobrar registrada correctamente");
        }

        [HttpPut("{id}/pagar")]
        public async Task<IActionResult> ActualizarPago(int id, [FromBody] bool pagado)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC sp_ActualizarEstadoPago {id}, {pagado}");
            return NoContent();
        }
    }
}
