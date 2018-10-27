using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using App1.Models;
using App1.Views;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace App1.ViewModels
{
   public class PaisItemViewModel:Pais
    {
        private Pais pais;
        public PaisItemViewModel()
        {
            this.pais = this;
        }

        public ICommand DetallePaisCommand { get { return new RelayCommand(DetallePais); } }

        private async void DetallePais()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PaisTabPage(pais));
        }
    }
}
