using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StripeTest.Startup))]
namespace StripeTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
