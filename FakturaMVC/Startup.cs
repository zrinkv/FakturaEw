using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FakturaMVC.Startup))]
namespace FakturaMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
