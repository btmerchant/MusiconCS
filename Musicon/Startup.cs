using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Musicon.Startup))]
namespace Musicon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
