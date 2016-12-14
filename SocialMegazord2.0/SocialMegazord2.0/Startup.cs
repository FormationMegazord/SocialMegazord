using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialMegazord2._0.Startup))]
namespace SocialMegazord2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
