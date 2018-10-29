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

        public static List<Cliente> ListaClientes { get; set; }

        public static LoginPage LoginPage { get; set; }

        public  App()
        {
            InitializeComponent();
            ListaClienteSqLite = new List<ClienteSqLite>();
            LoginPage = new LoginPage();
            dataService = new DataService();
            apiService = new ApiService();
            ListaClientes = new List<Cliente>();
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

        


        private async Task Sincronizar()
        {
            

            var a = await apiService.CheckConnection();
            if (a.IsSuccess)
            {
                await this.EliminarTodosClientes();
                await this.CargarClientes();
                this.ConvertirClientesSqlLiteDelService();
                await this.InsertarTodosClientes();

            }
        }

        private void ConvertirClientesSqlLiteDelService()
        {
            
        }

        private async Task InsertarTodosClientes()
        {
            var lista = ListaClientes.Select(x => new ClienteSqLite { Apellido = x.Apellido, ClienteId = x.ClienteId, Direccion = x.Direccion, Nombre = x.Nombre }).ToList();
            ListaClienteSqLite = lista;
            await dataService.Insert(ListaClienteSqLite);
            Settings.SincronizacionCompleta = true;
        }

        private async Task EliminarTodosClientes()
        {
            await dataService.EliminarTodosClientes();
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
