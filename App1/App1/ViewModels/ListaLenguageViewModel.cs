using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using App1.Common.Models;
using App1.Domain.ModelsResult;
using App1.Models;

namespace App1.ViewModels
{
    class ListaFacturasViewModel
    {
        public ObservableCollection<FacturaSqLite> Facturas { get; set; }

        public ListaFacturasViewModel(ObservableCollection<FacturaSqLite> facturas)
        {
            Facturas = facturas;
        }
    }
}
