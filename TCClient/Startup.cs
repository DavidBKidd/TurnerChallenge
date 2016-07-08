using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TCClient.Startup))]
namespace TCClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
