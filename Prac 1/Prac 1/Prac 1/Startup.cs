using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Prac_1.Startup))]
namespace Prac_1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
