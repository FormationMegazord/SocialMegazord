using Microsoft.Ajax.Utilities;
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
    public class EventController : Controller
    {
        private bool IsUserAuthorizedToEdit(Event Event)
        {
            bool isAdmin = this.User.IsInRole("Admin");
            bool isAuthor = Event.IsAuthora(User.Identity.Name);

            return isAdmin || isAuthor;
        }
        
        // GET: Event
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        [Authorize]
        public ActionResult List()
        {
            using (var database = new BlogDbContext())
            {
                // Get events from database
                var events = database.Events.Include(a => a.Author).ToList();
                return View(events);
            }
        }
        [Authorize]
        public ActionResult Options(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var database = new BlogDbContext())
            {

                // Get the events from database
                var events = database.Events.Where(a => a.Id == id).Include(a => a.Author).First();
                if (events == null)
                {
                    return HttpNotFound();
                }
                return View(events);
            }
        }
        [Authorize]
        public ActionResult Create ()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Event events)
        {
            if (ModelState.IsValid)
            {
                using (var database = new BlogDbContext())
                {
                    var authorId = database.Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First()
                        .Id;

                    var authorEmail = database.Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First()
                        .UserName;

                    events.AuthorId = authorId;
                    events.AuthorEmail = authorEmail;

                    database.Events.Add(events);
                    database.SaveChanges();

                    return RedirectToAction("List", "Event");


                }
            }
            return View("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BlogDbContext())
            {
                // Get user from database 
                var eventId = database.Events.Where(a => a.Id == id).Include(a => a.Author).First();

                if (eventId == null)
                {
                    return HttpNotFound();
                }
                // Get user events from database
                database.Events.Remove(eventId);
                database.SaveChanges();               
                return RedirectToAction("List", "Event");
            }
        }
    }
}