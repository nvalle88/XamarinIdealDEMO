using System.Collections.Generic;
using System.Threading.Tasks;
using App1.Common.Models;
using App1.Domain.ModelsResult;
using App1.Domain.Utils;
using App1.Services;
using App1.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App1
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static DataService dataService { get; set; }

        public static ApiService apiService { get; set; }
        public static List<ClienteSqLite> ListaClienteSqLite { get; set; }

        public static List<FacturaSqLite> ListaFacturaSqLite { get; set; }

        public static List<Cliente> ListaClientes { get; set; }

        public static List<Factura> ListaFacturas { get; set; }

        public static LoginPage LoginPage { get; set; }

        public static MenuPage MenuPage { get; set; }

        public  App()
        {
            InitializeComponent();
            ListaClienteSqLite = new List<ClienteSqLite>();
            LoginPage = new LoginPage();
            dataService = new DataService();
            apiService = new ApiService();
            ListaClientes = new List<Cliente>();
            ListaFacturas = new List<Factura>();
            ListaFacturaSqLite = new List<FacturaSqLite>();
            
            if (Settings.IsRemembered && !string.IsNullOrEmpty(Settings.UserASP))
            {
                
                Task.Run(() => this.Sincronizar()).Wait();
                
                Application.Current.MainPage = new MasterPage();

            }
            else
            {
                MainPage = new NavigationPage(LoginPage);
            }
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

        private async Task InsertarTodosClientes()
        {
            var lista = ListaClientes.Select(x => new ClienteSqLite { Apellido = x.Apellido, ClienteId = x.ClienteId, Direccion = x.Direccion, Nombre = x.Nombre }).ToList();
            ListaClienteSqLite = lista;
            await dataService.Insert(ListaClienteSqLite);
        }

        private async Task InsertarTodosFactura()
        {
            var lista = ListaFacturas.Select(x => new FacturaSqLite { FacturaId = x.FacturaId, ClienteId = x.ClienteId, Fecha = x.Fecha,Valor=x.Valor, Nombre = x.Nombre }).ToList();
            ListaFacturaSqLite = lista;
            await dataService.Insert(ListaFacturaSqLite);
        }

        private async Task EliminarTodosClientes()
        {
            await dataService.EliminarTodosClientes();
        }

        private async Task EliminarTodosFactura()
        {
            await dataService.EliminarTodosFactura();
        }

        private async Task CargarClientes()
        {
            var response = await apiService.GetList<Cliente>(Global.UrlBase, Global.RoutePrefix, Global.ListarClientes, Settings.TokenType, Settings.AccessToken);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                 Application.Current.MainPage=new NavigationPage(new LoginPage());
                return;
            }

            ListaClientes=(List<Cliente>)response.Result;
            
        }


        private async Task CargarFacturas()
        {
            var response = await apiService.GetList<Factura>(Global.UrlBase, Global.RoutePrefix, Global.ListarFacturas, Settings.TokenType, Settings.AccessToken,ListaClientes);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }
            ListaFacturas = (List<Factura>)response.Result;

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
