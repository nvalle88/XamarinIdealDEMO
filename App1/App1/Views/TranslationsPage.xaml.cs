using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using App1.Models;
using App1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TranslationsPage : ContentPage
	{
		public TranslationsPage (Translations translations)
		{
            BindingContext = new TranslationViewModel(translations);
			InitializeComponent ();
		}
	}
}