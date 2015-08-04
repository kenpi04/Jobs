using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Job.Web.Startup))]
namespace Job.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
