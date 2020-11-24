using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JamCentral.Startup))]
namespace JamCentral
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
