using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using App1.Models;
using App1.Services;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class PaisesViewModel:PropertyChangedClass
    {

        #region Servicios

        private ApiService apiService;

        #endregion

        #region Atributos
        private ObservableCollection<PaisItemViewModel> paises;
        private List<Pais> paisesList;
        private bool isRefreshing;
        private string filtro;
        
        #endregion

        #region propiedades
        public ObservableCollection<PaisItemViewModel> Paises
        {
            get { return this.paises; }
            set { SetValue(ref this.paises, value); }
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
        public PaisesViewModel()
        {
            this.IsRefreshing = true;
            this.apiService = new ApiService();
            this.CargarPaises();
        }


        #endregion



        #region Comandos

        public ICommand RefreshCommand { get { return new RelayCommand(CargarPaises); } }


        public ICommand BuscarCommand { get { return new RelayCommand(Buscar); } }

        

        #endregion


        #region Metodos

       

        private void Buscar()
        {
            if (string.IsNullOrEmpty(this.Filtro))
            {
                this.Paises = new ObservableCollection<PaisItemViewModel>(this.ConvertirPaisItem());
                return;
            }
            this.Paises = new ObservableCollection<PaisItemViewModel>(this.ConvertirPaisItem().Where(x=>x.Name.ToLower().Contains(this.Filtro.ToLower()) 
                                                                                || x.Capital.ToLower().Contains(this.Filtro.ToLower())));
            return;
        }

        private async void CargarPaises()
        {
            this.paisesList = new List<Pais>();
            this.IsRefreshing = true;
            var a= await apiService.CheckConnection();
            if (!a.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", a.Message, "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
            var response = await apiService.GetList<Pais>("http://restcountries.eu", "/rest", "/v2/all");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }
           
            this.paisesList= (List<Pais>)response.Result;
            this.Paises = new ObservableCollection<PaisItemViewModel>(this.ConvertirPaisItem());
            this.IsRefreshing = false;
        }

        private IEnumerable<PaisItemViewModel> ConvertirPaisItem()
        {
            return paisesList.Select(l => new PaisItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            });
        }
        #endregion
    }
}
