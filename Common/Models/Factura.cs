using System;
using System.ComponentModel.DataAnnotations;

namespace App1.Common.Models
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        [Required]
        public double Valor { get; set; }

        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}