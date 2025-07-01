using CoreSistemaMasajes.Data;
using CoreSistemaMasajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreSistemaMasajes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpleadoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> Get()
        {
            return await _context.Empleados
                .FromSqlRaw("EXEC sp_ObtenerEmpleados")
                .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleados = await _context.Empleados
                .FromSqlRaw("EXEC sp_ObtenerEmpleadoPorId @Id = {0}", id)
                .ToListAsync();

            var empleado = empleados.FirstOrDefault();

            if (empleado == null)
                return NotFound();

            return empleado;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Empleado emp)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC sp_InsertarEmpleado 
            @NombreEmpleado = {emp.NombreEmpleado},
            @ApellidoEmpleado = {emp.ApellidoEmpleado},
            @TelefonoEmpleado = {emp.TelefonoEmpleado},
            @Cargo = {emp.Cargo}");

            return Ok("Empleado insertado");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Empleado emp)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC sp_ActualizarEmpleado 
            @EmpleadoId = {id},
            @NombreEmpleado = {emp.NombreEmpleado},
            @ApellidoEmpleado = {emp.ApellidoEmpleado},
            @TelefonoEmpleado = {emp.TelefonoEmpleado},
            @Cargo = {emp.Cargo}");

            return Ok("Empleado actualizado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC sp_EliminarEmpleado 
            @EmpleadoId = {id}");

            return Ok("Empleado desactivado");
        }
    }

}
