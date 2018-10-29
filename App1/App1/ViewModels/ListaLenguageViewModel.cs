using System;
using System.Collections.Generic;
using System.Text;
using App1.Common.Models;
using App1.Models;

namespace App1.ViewModels
{
    class ListaLenguageViewModel
    {
        public List<Factura> Facturas { get; set; }

        public ListaLenguageViewModel(List<Factura> facturas)
        {
            Facturas = facturas;
        }
    }
}
