using SocialMegazord2._0.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SocialMegazord2._0.Controllers
{
    public class PostController : Controller
    {
        private bool IsUserAuthorizedToEdit(Post post)
        {
            bool isAdmin = this.User.IsInRole("Admin");
            bool isAuthor = post.IsAuthor(this.User.Identity.Name);

            return isAdmin || isAuthor;
        }

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

       public ActionResult List ()
        {
            using (var database = new BlogDbContext())
            {
                var entertainmentPost = database.Posts
                    .Include(p => p.CommunityId
                    .Equals(1))
                    .ToList();
                return View(entertainmentPost);
            }

            using (var database = new BlogDbContext())
            {
                var sciencePost = database.Posts
                    .Include(p => p.CommunityId
                    .Equals(2))
                    .ToList();
                return View(sciencePost);
            }



        }


        public ActionResult Create()
        {
            using (var database = new BlogDbContext())
            {
                var model = new PostViewModel();
                model.Communities = database.Communities.OrderBy(c => c.Name).ToList();

                return View(model);
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create (PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var database = new BlogDbContext())
                {
                    var authorId = database.Users.Where(u => u.UserName == this.User.Identity.Name).First().Id;

                    var post = new Post(authorId, model.Title, model.Content, model.CommunityId);

                    database.Posts.Add(post);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }
        // BATE SHE MINA S PRAHUSMUKASHKATA CHE TKA MAIKA MI MI OPQVA

        public ActionResult Edit (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            }

            using (var database = new BlogDbContext())
            {
                // Get posts from database 
                var post = database.Posts.Where(a => a.Id == id).First();
                // Check if post exists

                if (! IsUserAuthorizedToEdit(post))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                if (post == null)
                {
                    return HttpNotFound();
                }
                // Create the view model
                var model = new PostViewModel();
                model.Id = post.Id;
                model.Title = post.Title;
                model.Content = post.Content;
                model.CommunityId = post.CommunityId;
                model.Communities = database.Communities.OrderBy(c => c.Name).ToList();

                // Pass the view model to view 

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit (PostViewModel model)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                using (var database = new BlogDbContext())
                {
                    // Get posts from database
                    var post = database.Posts.FirstOrDefault(a => a.Id == model.Id);
                    // Set post properties
                    post.Title = model.Title;
                    post.Content = model.Content;
                    // Save post state in database
                    database.Entry(post).State = EntityState.Modified;
                    database.SaveChanges();
                    // Redirect to page
                    return RedirectToAction("Index");
                }
            }
            // If model state is invalid, return the same view

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BlogDbContext())
            {
                // Get post from database
                var post = database.Posts.Where(a => a.Id == id).Include(a => a.Author).First();
                // Check if post exists
                if (post == null)
                {
                    return HttpNotFound();
                }
                // Delete post from database
                database.Posts.Remove(post);
                // Redirect to some page
                return RedirectToAction("Index");
            }
        }
    }  
}