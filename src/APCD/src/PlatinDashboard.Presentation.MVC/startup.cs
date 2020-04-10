using Microsoft.Owin;
using Owin;
using PlatinDashboard.Presentation.MVC;

[assembly: OwinStartup(typeof(Startup))]
namespace PlatinDashboard.Presentation.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}