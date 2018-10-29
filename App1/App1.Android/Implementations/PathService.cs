
[assembly: Xamarin.Forms.Dependency(typeof(App1.Droid.Implementations.PathService))]

namespace App1.Droid.Implementations
{
    using Interfaces;
    using System;
    using System.IO;

    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "Facturas.db3");
        }
    }
}