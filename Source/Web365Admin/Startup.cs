using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web365Admin.Startup))]
namespace Web365Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
