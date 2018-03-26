using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmogAppWeb.Startup))]
namespace SmogAppWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
