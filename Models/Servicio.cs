using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CoreSistemaMasajes.Models
{
    public class Servicio
    {
        public int ServicioId { get; set; }
        [Required]
        [StringLength(100)]
        public string NombreServicio { get; set; }
        [Precision(10, 2)]
        public decimal PrecioServicio { get; set; }
        public int DuracionPromedioMinutos { get; set; }

        public bool Activo { get; set; } = true;
    }
}
