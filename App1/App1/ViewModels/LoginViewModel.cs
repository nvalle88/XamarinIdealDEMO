using System.Windows.Input;
using App1.Domain.Utils;
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



        private async void Login()
        {
            this.IsRunning = true;
            this.IsEnabled = false;
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar Email", "Aceptar");
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

            var conexion =await apiService.CheckConnection();
            if (!conexion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", conexion.Message, "Aceptar");
                return;
            }

            var token = await apiService.GetToken(Global.UrlBase,this.Email,this.Contrasena);
            if (token==null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error","Ha ocurrido un error, intente de nuevo", "Aceptar");
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
            await Application.Current.MainPage.Navigation.PushAsync(new PaisesPage());
            return;

        }



        #endregion
    }
}
