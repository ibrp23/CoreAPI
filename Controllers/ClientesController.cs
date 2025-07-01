using CoreSistemaMasajes.Models;
using CoreSistemaMasajes.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CoreSistemaMasajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> Get()
        {
            return await _context.Clientes
                .FromSqlRaw("EXEC sp_ObtenerClientes")
                .ToListAsync();
        }

        // GET: api/clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetCliente(int id)
        {
            var clientes = await _context.Clientes
                .FromSqlRaw("EXEC sp_ObtenerClientePorId @Id = {0}", id)
                .ToListAsync();

            var cliente = clientes.FirstOrDefault();

            if (cliente == null)
                return NotFound();

            return cliente;
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostCliente(Clientes cliente)
        {
            var parameters = new[]
            {
                new SqlParameter("@NombreCliente", cliente.NombreCliente),
                new SqlParameter("@ApellidoCliente", cliente.ApellidoCliente),
                new SqlParameter("@TelefonoCliente", cliente.TelefonoCliente),
                new SqlParameter("@CorreoCliente", cliente.CorreoCliente ?? (object)DBNull.Value),
                new SqlParameter("@FechaRegistro", cliente.FechaRegistro)
            };

            // Ejecutar SP y obtener el nuevo ID
            var nuevoId = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_InsertarCliente @NombreCliente, @ApellidoCliente, @TelefonoCliente, @CorreoCliente, @FechaRegistro",
                parameters);

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        
        // PUT: api/clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Clientes cliente)
        {
            if (id != cliente.Id) return BadRequest();

            var parameters = new[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@NombreCliente", cliente.NombreCliente),
                new SqlParameter("@ApellidoCliente", cliente.ApellidoCliente),
                new SqlParameter("@TelefonoCliente", cliente.TelefonoCliente),
                new SqlParameter("@CorreoCliente", cliente.CorreoCliente ?? (object)DBNull.Value)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_ActualizarCliente @Id, @NombreCliente, @ApellidoCliente, @TelefonoCliente, @CorreoCliente",
                parameters);

            return NoContent();
        }

        // DELETE: api/clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var param = new SqlParameter("@Id", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_EliminarCliente @Id", param);
            return NoContent();
        }
    }
}
