using System.ComponentModel.DataAnnotations;

namespace CoreSistemaMasajes.Models
{
    //clase para el login
    public class Usuario
    {
        
        public int UsuarioId { get; set; }
        [Required]
        [StringLength(50)]
        public string UsuarioNombre { get; set; }
        [Required]
        [StringLength(20)]
        public string ClaveUsuario { get; set; }

        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime UltimoLogin { get; set; } = DateTime.Now;

        // Relación con Empleado (opcional)
        public int? EmpleadoId { get; set; }
        public Empleado? Empleado { get; set; }
    }
}
