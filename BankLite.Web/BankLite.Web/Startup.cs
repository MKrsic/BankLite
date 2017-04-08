using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankLite.Web.Startup))]
namespace BankLite.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
