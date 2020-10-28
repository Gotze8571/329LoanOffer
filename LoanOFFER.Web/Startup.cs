using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoanOFFER.Web.Startup))]
namespace LoanOFFER.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
