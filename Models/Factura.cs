using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreSistemaMasajes.Models
{
    public class Factura
    {
        public int FacturaId { get; set; }

        public int ClienteId { get; set; }
        [JsonIgnore]
        public Clientes? Cliente { get; set; }
    

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Precision(10,2)]
        public decimal Total { get; set; }

        [Required]
        [StringLength(13)]
        public string TipoPago { get; set; } // tarjeta, efectivo, transferencia

        public ICollection<FacturaDetalle> Detalles { get; set; } = new List<FacturaDetalle>();
    }
}
