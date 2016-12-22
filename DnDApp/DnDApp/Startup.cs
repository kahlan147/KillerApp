using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DnDApp.Startup))]
namespace DnDApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
