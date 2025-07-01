using System.ComponentModel.DataAnnotations;

namespace CoreSistemaMasajes.Models
{
    public class Clientes
    {
        public int Id { get; set; } // Clave primaria
        [Required]
        [StringLength(50)]
        public string NombreCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string ApellidoCliente { get; set; }
        [Required]
        [StringLength(13)]
        public string TelefonoCliente { get; set; }
        
        [StringLength(150)]
        public string? CorreoCliente { get; set; } // Opcional

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
