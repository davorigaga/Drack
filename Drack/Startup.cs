using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Drack.Startup))]
namespace Drack
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
