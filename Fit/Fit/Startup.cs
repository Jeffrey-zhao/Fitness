using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fit.Startup))]
namespace Fit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
