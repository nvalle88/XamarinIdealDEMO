using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Common.Models;
using App1.Domain.ModelsResult;
using App1.Domain.Utils;
using App1.Helpers;
using App1.Services;
using App1.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class LoginViewModel : PropertyChangedClass
    {

        #region Servicios

        private ApiService apiService;

        #endregion


        #region Atributos

        private string contrasena ;
        
        private bool isRunning ;
        
        private bool isEnabled;

        #endregion

        #region Propiedades


        public string Email { get; set; }
        public string Contrasena
        {
            get { return this.contrasena; }
            set { SetValue(ref this.contrasena, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool Recuerdame { get; set; }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        #endregion

        #region Constructores

        public LoginViewModel()
        {
            apiService = new ApiService();
            Email = string.Empty;
            Contrasena =string.Empty;
            Recuerdame = true;
            IsEnabled = true;


        }
        #endregion


        #region Comandos

        public ICommand LoginCommand { get { return new RelayCommand(Login); } }







        #endregion

        #region Metodos
        private async void Login()
        {
            this.IsRunning = true;
            this.IsEnabled = false;
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Aceptar, Languages.ValidacionEmail,Languages.Aceptar);
                this.IsRunning = false;
                this.IsEnabled = true;
                return;
            }

            if (string.IsNullOrEmpty(this.Contrasena))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar la contraseña", "Aceptar");
                return;
            }

            var conexion = await apiService.CheckConnection();
            if (!conexion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", conexion.Message, "Aceptar");
                return;
            }

            var token = await apiService.GetToken(Global.UrlBase, this.Email, this.Contrasena);
            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error, intente de nuevo", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", token.ErrorDescription, "Aceptar");
                this.Contrasena = string.Empty;
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;
            Settings.TokenType = token.TokenType;
            Settings.AccessToken = token.AccessToken;
            Settings.UserASP = token.UserName;
            Settings.IsRemembered = this.Recuerdame;
            Task.Run(() => this.Sincronizar()).Wait();
            Application.Current.MainPage = new MasterPage();
            return;
        }

        public async Task Sincronizar()
        {
            var a = await apiService.CheckConnection();
            if (a.IsSuccess)
            {
                await this.EliminarTodosClientes();
                await this.CargarClientes();
                await this.InsertarTodosClientes();

                await this.EliminarTodosFactura();
                await this.CargarFacturas();
                await this.InsertarTodosFactura();
            }
        }


        private async Task InsertarTodosFactura()
        {
            var lista =App.ListaFacturas.Select(x => new FacturaSqLite { FacturaId = x.FacturaId, ClienteId = x.ClienteId, Fecha = x.Fecha, Valor = x.Valor, Nombre = x.Nombre }).ToList();
            App.ListaFacturaSqLite = lista;
            await App.dataService.Insert(App.ListaFacturaSqLite);
        }
        private async Task CargarFacturas()
        {
            var response = await apiService.GetList<Factura>(Global.UrlBase, Global.RoutePrefix, Global.ListarFacturas, Settings.TokenType, Settings.AccessToken, App.ListaClientes);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }
            App.ListaFacturas = (List<Factura>)response.Result;

        }
        private async Task InsertarTodosClientes()
        {
            var lista = App.ListaClientes.Select(x => new ClienteSqLite { Apellido = x.Apellido, ClienteId = x.ClienteId, Direccion = x.Direccion, Nombre = x.Nombre }).ToList();
            App.ListaClienteSqLite = lista;
            await App.dataService.Insert(App.ListaClienteSqLite);
            Settings.SincronizacionCompleta = true;
        }

        private async Task EliminarTodosClientes()
        {
            await App.dataService.EliminarTodosClientes();
        }

        private async Task EliminarTodosFactura()
        {
            await App.dataService.EliminarTodosFactura();
        }
        private async Task CargarClientes()
        {
            var response = await apiService.GetList<Cliente>(Global.UrlBase, Global.RoutePrefix, Global.ListarClientes, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            App.ListaClientes = (List<Cliente>)response.Result;

        }
        #endregion
    }
}
