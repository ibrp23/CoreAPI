using CoreSistemaMasajes.Data;
using CoreSistemaMasajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreSistemaMasajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController: ControllerBase 
    {
        private readonly AppDbContext _context;

        public ProductoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos
                .FromSqlRaw("EXEC sp_ObtenerProductos")
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC sp_InsertarProducto 
                        @NombreProducto={producto.NombreProducto}, 
                        @DescripcionProducto={producto.DescripcionProducto}, 
                        @PrecioProducto={producto.PrecioProducto}, 
                        @Stock={producto.Stock}, 
                        @Disponible={producto.Disponible}");
   

            return Ok("Producto insertado correctamente");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {

            await _context.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC sp_ActualizarProducto 
                    @ProductoId={producto.ProductoId}, 
                    @NombreProducto={producto.NombreProducto}, 
                    @DescripcionProducto={producto.DescripcionProducto}, 
                    @PrecioProducto={producto.PrecioProducto}, 
                    @Stock={producto.Stock}, 
                    @Disponible={producto.Disponible}");

            return Ok("Producto actualizado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC sp_EliminarProducto @ProductoId={id}");

            return NoContent();
        }
    }
}
