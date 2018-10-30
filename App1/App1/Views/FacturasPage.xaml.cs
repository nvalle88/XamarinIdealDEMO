using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Common.Models;
using App1.Domain.ModelsResult;
using App1.Models;
using App1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FacturasPage : ContentPage
	{
		public FacturasPage(ObservableCollection<FacturaSqLite> lista)
		{
            BindingContext = new ListaFacturasViewModel(lista);
			InitializeComponent ();
            
		}
	}
}