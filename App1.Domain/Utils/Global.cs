using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Domain.Utils
{
   public static class Global
    {
        public static string UrlBase { get {  return "https://demoidealapi.azurewebsites.net"; } set {; } }
        public static string RoutePrefix { get { return "/Api"; } set {; } }

        public static string ListarClientes { get { return "/Cliente/ListaCliente"; } set {; } }
    }
}
