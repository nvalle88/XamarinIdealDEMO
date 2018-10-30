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
	public partial class ClienteTabPage : TabbedPage
	{
		public ClienteTabPage(Cliente cliente)
		{
            Children.Add(new ClientePage(cliente));
            //Children.Add(new BordesPage(pais.Borders));
            var listaFacturas=new ObservableCollection<FacturaSqLite>(App.ListaFacturaSqLite.Where(x => x.ClienteId == cliente.ClienteId).ToList());
            Children.Add(new FacturasPage(listaFacturas));
            //Children.Add(new MonedasPage(pais.Currencies));
            //Children.Add(new TranslationsPage(pais.Translations));
            //BindingContext = new PaisViewModel(pais);
            InitializeComponent();

        }
	}
}