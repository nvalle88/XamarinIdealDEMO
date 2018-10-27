using System;
using System.Collections.Generic;
using System.Text;
using App1.Models;

namespace App1.ViewModels
{
    class ListaLenguageViewModel
    {
        public List<Language> Languages { get; set; }

        public ListaLenguageViewModel(List<Language> lista)
        {
            Languages = lista;
        }
    }
}
