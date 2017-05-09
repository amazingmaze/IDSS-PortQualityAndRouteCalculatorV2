using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IDSS_RouteAndQualityForShippers.Startup))]
namespace IDSS_RouteAndQualityForShippers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
