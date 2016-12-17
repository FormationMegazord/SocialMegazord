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
        // GET: Event
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            using (var database = new BlogDbContext())
            {
                // Get events from database
                var events = database.Events.Include(a => a.Author).ToList();
                return View(events);
            }
        }

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
        [HttpPost]
        public ActionResult Create(Event Event)
        {
            if (ModelState.IsValid)
            {
                using (var database = new BlogDbContext())
                {
                    var authorId = database.Users
                        .Where(u => u.Name == this.User.Identity.Name)
                        .First()
                        .Id;

                    Event.AuthorId = authorId;

                    database.Events.Add(Event);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(Event);
        }
    }
}