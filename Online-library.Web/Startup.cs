using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Online_library.Web.Startup))]
namespace Online_library.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
