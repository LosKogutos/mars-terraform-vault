using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarsTerraform.Startup))]
namespace MarsTerraform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
