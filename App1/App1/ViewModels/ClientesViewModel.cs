using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Common.Models;
using App1.Domain.ModelsResult;
using App1.Domain.Utils;
using App1.Services;
using App1.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ClientesViewModel:PropertyChangedClass
    {

        #region Servicios

       
        

        #endregion

        #region Atributos
        private ObservableCollection<ClienteItemViewModel> clientes;
        private bool isRefreshing;
        private string filtro;
        
        #endregion

        #region propiedades
        public ObservableCollection<ClienteItemViewModel> Clientes
        {
            get { return this.clientes; }
            set { SetValue(ref this.clientes, value); }
        }

        public string Filtro
        {
            get { return this.filtro; }
            set
            {
                SetValue(ref this.filtro, value);
                this.Buscar();
            }
        }



        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Constructores
        public ClientesViewModel()
        {
            this.IsRefreshing = true;
            Task.Run(() => this.CargarClientes()).Wait();
            this.IsRefreshing = false;

        }


        #endregion



        #region Comandos

        public ICommand RefreshCommand { get { return new RelayCommand(CargarClientes); } }


        public ICommand BuscarCommand { get { return new RelayCommand(Buscar); } }

        

        #endregion


        #region Metodos

       

        private void Buscar()
        {
            if (string.IsNullOrEmpty(this.Filtro))
            {
                this.Clientes = new ObservableCollection<ClienteItemViewModel>(this.ConvertirClienteItem());
                return;
            }
            this.Clientes = new ObservableCollection<ClienteItemViewModel>(this.ConvertirClienteItem().Where(x=>x.Nombre.ToLower().Contains(this.Filtro.ToLower()) 
                                                                                || x.Apellido.ToLower().Contains(this.Filtro.ToLower()) 
                                                                                || x.Direccion.ToLower().Contains(this.Filtro.ToLower())));
            return;
        }

        private async void CargarClientes()
        {
           
            //this.clientesList = new List<Cliente>();
            //this.clientesSqLiteList = new List<ClienteSqLite>();
            this.IsRefreshing = true;
            var a= await App.apiService.CheckConnection();
            if (!a.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", a.Message, "Aceptar");
                Application.Current.MainPage= new NavigationPage(App.LoginPage) ;
                return;
            }
            //var response = await apiService.GetList<Cliente>(Global.UrlBase, Global.RoutePrefix, Global.ListarClientes,Settings.TokenType,Settings.AccessToken);
            //if (!response.IsSuccess)
            //{
            //    this.IsRefreshing = false;
            //    await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
            //    await Application.Current.MainPage.Navigation.PopAsync();
            //    return;
            //}

            //this.paisesList= (List<Cliente>)response.Result;

            //clientesSqLiteList= await  this.dataService.ListarClientes();
            this.Clientes = new ObservableCollection<ClienteItemViewModel>(this.ConvertirClienteItem());
            this.IsRefreshing = false;
        }

       

        private IEnumerable<ClienteItemViewModel> ConvertirClienteItem()
        {

            return App.ListaClienteSqLite.Select(l => new ClienteItemViewModel
            {
                Apellido = l.Apellido,
                Nombre = l.Nombre,
                Direccion = l.Direccion,
                ClienteId = l.ClienteId,
               
            });
        }
        #endregion
    }
}
