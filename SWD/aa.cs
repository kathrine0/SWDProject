using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SWD.Startup))]
namespace SWD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
