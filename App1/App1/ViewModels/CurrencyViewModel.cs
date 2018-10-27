using System;
using System.Collections.Generic;
using System.Text;
using App1.Models;

namespace App1.ViewModels
{
    class CurrencyViewModel
    {
        public List<Currency> Currencies { get; set; }

        public CurrencyViewModel(List<Currency> lista)
        {
            Currencies = lista;
        }
    }
}
