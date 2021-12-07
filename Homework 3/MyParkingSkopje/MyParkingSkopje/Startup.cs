using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyParkingSkopje.Startup))]
namespace MyParkingSkopje
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
