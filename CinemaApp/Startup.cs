using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CinemaApp.Startup))]
namespace CinemaApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
