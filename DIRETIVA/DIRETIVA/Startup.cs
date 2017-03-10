using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DIRETIVA.Startup))]
namespace DIRETIVA
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
