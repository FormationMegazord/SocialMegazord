using Blog.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SocialMegazord2._0;
using SocialMegazord2._0.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        public ActionResult EditUsers()
        {
            using (var db = new BlogDbContext())
            {
                var Users = db.Users.ToList();
                return View(Users);
            }
            
        }
        public ActionResult EditPosts()
        {
            using (var db = new BlogDbContext())
            {
                var Posts = db.Posts.Include(u => u.Author).ToList();
                return View(Posts);
            }
        }

        public ActionResult EditEvents()
        {
            using (var db = new BlogDbContext())
            {
                var Events = db.Events.Include(a => a.Author).ToList();
                return View(Events);
            }
        }
        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("Index");
        }

        public ActionResult List()
        {
            using (var database = new BlogDbContext())
            {
                var users = database.Users.ToList();
                var admins = GetAdminUserNames(users, database);
                ViewBag.Admins = admins;

                return View(users);
            }
        }
        public HashSet<string> GetAdminUserNames(List<ApplicationUser> users, BlogDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var admins = new HashSet<string>();

            foreach (var user in users)
            {
                if (userManager.IsInRole(user.Id, "Admin"))
                {
                    admins.Add(user.UserName);
                }
            }
            return admins;
        }

        public ActionResult Edit(string id)
        {
            // Validate id
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var database = new BlogDbContext())
            {
                // GET user from database
                var user = database.Users.Where(u => u.Id == id).First();
                // Check if user exists
                if (user == null)
                {
                    return HttpNotFound();
                }
                // Create a view model
                var viewModel = new EditUserViewModel();
                viewModel.User = user;
                viewModel.Roles = GetUserRoles(user, database);
                // Pass the model to the view 
                return View(viewModel);
            }
        }
        private List<Role> GetUserRoles(ApplicationUser user, BlogDbContext db)
        {
            // Create user manager
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            // Get all application roles
            var roles = db.Roles.Select(r => r.Name).OrderBy(r => r).ToList();
            // For each application role, check if the user has it
            var userRoles = new List<Role>();

            foreach (var roleName in roles)
            {
                var role = new Role { Name = roleName };

                if (userManager.IsInRole(user.Id, roleName))
                {
                    role.IsSelected = true;
                }
                userRoles.Add(role);
            }
            // Return a list with all roles
            return userRoles;
        }

        [HttpPost]
        public ActionResult Edit(string id, EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var database = new BlogDbContext())
                {
                    // Get user from database 
                    var user = database.Users.FirstOrDefault(u => u.Id == id);
                    // Check if users exists
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    // If password field is not empty, change password
                    if (!string.IsNullOrEmpty(viewModel.Password))
                    {
                        var hasher = new PasswordHasher();
                        var passwordHash = hasher.HashPassword(viewModel.Password);
                        user.PasswordHash = passwordHash;
                    }
                    // Set user properties
                    user.Email = viewModel.User.Email;
                    user.Name = viewModel.User.Name;
                    user.UserName = viewModel.User.Email;
                    SetUserRoles(viewModel, user, database);
                    // Save changes
                    database.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    database.SaveChanges();

                }
            }
            return RedirectToAction("List");
        }
        public void SetUserRoles(EditUserViewModel viewModel, ApplicationUser user, BlogDbContext context)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            foreach (var role in viewModel.Roles)
            {
                if (role.IsSelected && !userManager.IsInRole(user.Id, role.Name))
                {
                    userManager.AddToRole(user.Id, role.Name);
                }
                else if (!role.IsSelected && userManager.IsInRole(user.Id, role.Name))
                {
                    userManager.RemoveFromRole(user.Id, role.Name);
                }
            }
        }

        //public ActionResult DeleteUser(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    using (var database = new BlogDbContext())
        //    {
        //        //Get user from database
        //        var user = database.Users.Where(u => u.Id.Equals(id)).First();
        //        // Check if user exists
        //        if (user == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(user);
        //    }
        //}

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BlogDbContext())
            {
                // Get user from database 
                var user = database.Users.Find(id);
                // Get user articles from database
                var userPosts = database.Posts.Where(a => a.Author.Id == user.Id);
                // Delete user articles 
                foreach (var posts in userPosts)
                {
                    database.Posts.Remove(posts);
                }
                // Delete user and save changes
                database.Users.Remove(user);
                database.SaveChanges();

                return RedirectToAction("Index", "");
            }
        }
    }
}