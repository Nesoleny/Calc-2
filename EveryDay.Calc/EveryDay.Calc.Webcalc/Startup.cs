using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EveryDay.Calc.Webcalc.Startup))]
namespace EveryDay.Calc.Webcalc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
