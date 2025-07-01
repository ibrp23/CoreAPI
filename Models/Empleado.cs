using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreSistemaMasajes.Models
{
    public class Empleado
    {
        public int EmpleadoId { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreEmpleado { get; set; }
        [Required]
        [StringLength(50)]
        public string ApellidoEmpleado { get; set; }
        [Required]
        [StringLength(13)]
        public string TelefonoEmpleado { get; set; }
        [Required]
        [StringLength(100)]
        public string Cargo { get; set; } // Masajista, recepcionista, etc.
        public bool Activo { get; set; } = true;

        [JsonIgnore]
        public ICollection<Cita>? Citas { get; set; } // Relación 1 a muchos

    }
}
