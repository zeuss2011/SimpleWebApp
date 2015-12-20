using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppWithAuthentification.Startup))]
namespace WebAppWithAuthentification
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
