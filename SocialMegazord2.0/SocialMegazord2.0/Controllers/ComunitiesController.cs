using SocialMegazord2._0.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialZord_project.Controllers
{
    public class ComunitiesController : Controller
    {
        public ActionResult Entertainment(Communities com)
        {
            using (var db = new BlogDbContext())
            {
                var comId = com.Id;
                comId = 1;
                var posts = db.Posts.Include(p => p.Author).Where(p => p.CommunityId == comId).ToList();
                return View(posts);
            }
        }

        public ActionResult Science(Communities com)
        {
            using (var db = new BlogDbContext())
            {
                var comId = com.Id;
                comId = 2;
                var posts = db.Posts.Include(p => p.Author).Where(p => p.CommunityId == comId).ToList();
                return View(posts);
            }
        }

        public ActionResult MutualHelp(Communities com)
        {
            using (var db = new BlogDbContext())
            {
                var comId = com.Id;
                comId = 3;
                var posts = db.Posts.Include(p => p.Author).Where(p => p.CommunityId == comId).ToList();
                return View(posts);
            }
        }

        public ActionResult List()
        {
            using (var database = new BlogDbContext())
            {
                var communities = database.Communities.ToList();

                return View(communities); 
            }
        }
        
    }

    
}