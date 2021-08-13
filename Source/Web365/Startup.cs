using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web365.Startup))]
namespace Web365
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
