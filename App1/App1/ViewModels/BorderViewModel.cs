using System;
using System.Collections.Generic;
using System.Text;
using App1.Models;

namespace App1.ViewModels
{
    class BorderViewModel
    {
        public List<Border> Borders { get; set; }

        public BorderViewModel(List<string> lista)
        {
            Borders = new List<Border>();
            if (lista.Count==0)
            {
                Borders.Add(new Border { Code = "SIN FRONTERAS" });
                return;
            }
            foreach (var item in lista)
            {
                Borders.Add(new Border { Code = item });
            }
        }
    }
}
