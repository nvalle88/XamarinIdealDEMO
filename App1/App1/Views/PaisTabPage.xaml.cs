using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Models;
using App1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaisTabPage : TabbedPage
	{
		public PaisTabPage (Pais pais)
		{
            Children.Add(new PaisPage(pais));
            Children.Add(new BordesPage(pais.Borders));
            Children.Add(new LenguajePage(pais.Languages));
            Children.Add(new MonedasPage(pais.Currencies));
            Children.Add(new TranslationsPage(pais.Translations));
            BindingContext = new PaisViewModel(pais);
            InitializeComponent();

        }
	}
}