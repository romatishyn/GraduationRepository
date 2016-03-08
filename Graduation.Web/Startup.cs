using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Graduation.Web.Startup))]
namespace Graduation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
