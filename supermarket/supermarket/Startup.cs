using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(supermarket.Startup))]
namespace supermarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
