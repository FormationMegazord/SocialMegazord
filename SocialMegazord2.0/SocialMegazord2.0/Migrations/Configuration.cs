namespace SocialMegazord2._0.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<SocialMegazord2._0.Models.BlogDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SocialMegazord2._0.Models.BlogDbContext context)
        {

            if (!context.Roles.Any())
            {
                this.CreateRole(context, "Admin");
                this.CreateRole(context, "User");
            }

            if (!context.Users.Any())
            {
                this.CreateUser(context, "admin@admin.com", "Admin", "123");
                this.SetRoleToUser(context, "admin@admin.com", "Admin");
            }

            if (!context.Communities.Any(c => c.Name == "Entertainment"))
            {
                Communities entertainment = new Communities()
                {
                    Name = "Entertainment"
                };
                context.Communities.Add(entertainment);
                context.SaveChanges();
            }
            if (!context.Communities.Any(c => c.Name == "Science"))
            {
                Communities science = new Communities()
                {
                    Name = "Science"
                };
                context.Communities.Add(science);
                context.SaveChanges();
            }
            if (!context.Communities.Any(c => c.Name == "Mutual Help"))
            {
                Communities mutualHelp = new Communities()
                {
                    Name = "Mutual Help"
                };
                context.Communities.Add(mutualHelp);
                context.SaveChanges();
            }
        }


        private void CreateRole(BlogDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));


            var result = roleManager.Create(new IdentityRole(roleName));

            if (!result.Succeeded) 
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }

        private void CreateUser(BlogDbContext context, string email, string name, string password)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireDigit = false,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false,
            };

            var admin = new ApplicationUser
            {
                UserName = email,
                Name = name,
                Email = email
            };

            var result = userManager.Create(admin, password);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }

        private void SetRoleToUser(BlogDbContext context, string email, string role)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            var user = context.Users.Where(u => u.Email == email).First();

            var result = userManager.AddToRole(user.Id, role);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }
    }
}
