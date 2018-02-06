using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LI4.Startup))]
namespace LI4
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
