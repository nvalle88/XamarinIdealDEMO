using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App1.Common.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public virtual List<Factura>  Facturas { get; set; }
    }
}