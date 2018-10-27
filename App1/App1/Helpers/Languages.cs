using App1.Interfaces;
using Xamarin.Forms;
using App1.Resources;

namespace App1.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Aceptar { get { return Resource.Aceptar; } }
        public static string Error { get { return Resource.Error; } }
        public static string Recuerdame { get { return Resource.Recuerdame; } }
        public static string ValidacionEmail { get { return Resource.ValidacionEmail; } }



    }
}
