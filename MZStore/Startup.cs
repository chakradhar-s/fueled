using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MZStore.Startup))]
namespace MZStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
