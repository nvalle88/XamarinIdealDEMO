using System;
using System.Collections.Generic;
using System.Text;
using App1.Common.Models;
using App1.Models;
using App1.Views;
using Xamarin.Forms;

namespace App1.ViewModels
{
   public class ClienteViewModel
    {
        public Cliente Cliente { get; set; }
        public ClienteViewModel(Cliente cliente)
        {
            this.Cliente = cliente;
        }
    }
}
