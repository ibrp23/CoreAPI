using CoreSistemaMasajes.Data;
using CoreSistemaMasajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CoreSistemaMasajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FacturaController: ControllerBase
    {
        private readonly AppDbContext _context;

        public FacturaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            return await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Detalles)
                .ToListAsync();
        }

        // GET para traer factura con detalles
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.FacturaId == id);

            if (factura == null)
                return NotFound();

            return factura;
        }

        [HttpPost]
        public async Task<IActionResult> PostFactura(Factura factura)
        {
            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var transaction = conn.BeginTransaction();
            try
            {
                using var cmdFactura = conn.CreateCommand();
                cmdFactura.Transaction = transaction;
                cmdFactura.CommandText = "sp_InsertarFactura";
                cmdFactura.CommandType = CommandType.StoredProcedure;

                cmdFactura.Parameters.Add(new SqlParameter("@ClienteId", factura.ClienteId));
                cmdFactura.Parameters.Add(new SqlParameter("@Fecha", factura.Fecha));
                cmdFactura.Parameters.Add(new SqlParameter("@Total", factura.Total));
                cmdFactura.Parameters.Add(new SqlParameter("@TipoPago", factura.TipoPago));

                var outputIdParam = new SqlParameter("@NuevoId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmdFactura.Parameters.Add(outputIdParam);

                await cmdFactura.ExecuteNonQueryAsync();
                int nuevoFacturaId = (int)outputIdParam.Value;

                foreach (var detalle in factura.Detalles)
                {
                    using var cmdDetalle = conn.CreateCommand();
                    cmdDetalle.Transaction = transaction;
                    cmdDetalle.CommandText = "sp_InsertarFacturaDetalle";
                    cmdDetalle.CommandType = CommandType.StoredProcedure;

                    cmdDetalle.Parameters.Add(new SqlParameter("@FacturaId", nuevoFacturaId));
                    cmdDetalle.Parameters.Add(new SqlParameter("@Tipo", detalle.Tipo));
                    cmdDetalle.Parameters.Add(new SqlParameter("@NombreItem", detalle.NombreItem));
                    cmdDetalle.Parameters.Add(new SqlParameter("@PrecioUnitario", detalle.PrecioUnitario));
                    cmdDetalle.Parameters.Add(new SqlParameter("@Cantidad", detalle.Cantidad));
                    cmdDetalle.Parameters.Add(new SqlParameter("@Subtotal", detalle.Subtotal));

                    await cmdDetalle.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
                return Ok(new { FacturaId = nuevoFacturaId });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
                return NotFound();

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
