using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace CoreSistemaMasajes.Models
{
    public class CuentaPorCobrar
    {
        public int Id { get; set; }

        public int FacturaId { get; set; }
        [JsonIgnore] // evita error de ciclo o validación en Swagger
        public Factura? Factura { get; set; } // ← ponerla como opcional

        [Precision(10, 2)]
        public decimal MontoPendiente { get; set; }

        public bool Pagado { get; set; } = false;
    }
}
