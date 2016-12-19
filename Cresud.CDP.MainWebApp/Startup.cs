using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cresud.CDP.MainWebApp.Startup))]
namespace Cresud.CDP.MainWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
