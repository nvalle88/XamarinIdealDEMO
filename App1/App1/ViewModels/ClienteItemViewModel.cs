using System.Windows.Input;
using App1.Common.Models;
using App1.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ClienteItemViewModel:Cliente
    {
        private Cliente cliente;
        public ClienteItemViewModel()
        {
            this.cliente = this;
        }

        public ICommand DetalleClienteCommand { get { return new RelayCommand(DetalleCliente); } }

        private async void DetalleCliente()
        {
            await App.Navigator.PushAsync(new ClienteTabPage(cliente));
        }
    }
}
