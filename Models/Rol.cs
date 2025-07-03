using System.ComponentModel.DataAnnotations;

namespace CoreSistemaMasajes.Models
{
    //rol para el usuario (login)
    public class Rol
    {
        public int RolId { get; set; }
        [Required]
        [StringLength(50)]
        public string NombreRol { get; set; } // Admin, Consulta, Mantenimiento

        
    }
}
