using System;

namespace App1.Domain.ModelsResult
{
    public class FacturaSqLite
    {
        public int FacturaId { get; set; }

        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public double Valor { get; set; }
        public int ClienteId { get; set; }

    }
}