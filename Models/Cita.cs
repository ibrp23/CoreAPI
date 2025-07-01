using System.ComponentModel.DataAnnotations;

namespace CoreSistemaMasajes.Models
{
    public class Cita
    {
        public int CitaId { get; set; }

        public int ClienteId { get; set; }
        public Clientes? Cliente { get; set; }  // ← Lo marcamos como opcional (no requerido en POST)

        public int ServicioId { get; set; }
        public Servicio? Servicio { get; set; } // ← Opcional también

        public int? EmpleadoId { get; set; } // Puede no estar asignado aún
        public Empleado? Empleado { get; set; } // ← Opcional

        public DateTime FechaHoraCita { get; set; }

        public DateTime FechaHoraIngresado { get; set; }

        [Required]
        [StringLength(9)]  
        public string Estado { get; set; } = "Reservada"; // Reservada, Cancelada, Realizada
        [Required]
        [StringLength(300)]
        public string? Observaciones { get; set; }
    }
}
