using Microsoft.Owin;
using Owin;
using SocialMegazord2._0.Migrations;
using SocialMegazord2._0.Models;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(SocialMegazord2._0.Startup))]
namespace SocialMegazord2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogDbContext, Configuration>());

            ConfigureAuth(app);
        }
    }
}
