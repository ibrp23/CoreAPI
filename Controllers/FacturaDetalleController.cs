using CoreSistemaMasajes.Data;
using CoreSistemaMasajes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreSistemaMasajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FacturaDetalleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FacturaDetalleController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaDetalle>>> GetDetalles()
        {
            return await _context.FacturaDetalles.ToListAsync();
        }


        
    }
}
