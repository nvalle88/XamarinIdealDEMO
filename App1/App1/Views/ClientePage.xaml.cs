using App1.Common.Models;
using App1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClientePage : ContentPage
	{
		public ClientePage(Cliente cliente)
		{
            BindingContext = new ClienteViewModel(cliente);
			InitializeComponent ();
		}
	}
}