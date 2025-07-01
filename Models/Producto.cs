using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CoreSistemaMasajes.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreProducto { get; set; }
        [Required]
        [StringLength(200)]
        public string DescripcionProducto { get; set; }
        [Precision(10, 2)]
        public decimal PrecioProducto { get; set; }

        public int Stock { get; set; } // Cantidad en existencia

        public bool Disponible { get; set; } = true;
    }
}
