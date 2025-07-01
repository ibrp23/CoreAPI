namespace CoreSistemaMasajes.Models
{
    public class HistorialSistema
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public string Accion { get; set; } // "Creó cliente", "Agendó cita", etc.

        public DateTime FechaHora { get; set; } = DateTime.Now;
    }
}
