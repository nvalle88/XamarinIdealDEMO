using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(App1.Backend.Startup))]
namespace App1.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
