using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoreSistemaMasajes.Models
{
    public class FacturaDetalle
    {
        public int Id { get; set; }

        public int FacturaId { get; set; }

        [JsonIgnore]
        public Factura? Factura { get; set; }
        [Required]
        [StringLength(8)]
        public string Tipo { get; set; } // "Servicio" o "Producto"
        [Required]
        [StringLength(100)]
        public string NombreItem { get; set; }
        [Precision(10, 2)]
        public decimal PrecioUnitario { get; set; }
        [Precision(10, 2)]
        public int Cantidad { get; set; }
        [Precision(10, 2)]
        public decimal Subtotal { get; set; }
    }

}