using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineCommerce_WEB.Startup))]
namespace OnlineCommerce_WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
