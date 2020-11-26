using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RAS.TimeSheets.MVC.Startup))]
namespace RAS.TimeSheets.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
