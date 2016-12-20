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
                // Get events from database
                var events = database.Events.Where(a => a.Id == id).Include(a=> a.Author).First();
                // Check if event exists

                if (!IsUserAuthorizedToEdit(events))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                if (events == null)
                {
                    return HttpNotFound();
                }
                // Create the view model
                database.Events.Remove(events);
                database.SaveChanges();

                // Pass the view model to view 

                return RedirectToAction("List", "Event");
            }
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            using (var database = new BlogDbContext())
            {
                // Get article from the database
                // var article = database.Articles.Include(a => a.Author).FirstOrDefault(a => a.Id == id);
                var events = database.Events.Find(id);
                // Check if article exists 
                if (!IsUserAuthorizedToEdit(events))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                if (events == null)
                {
                    return HttpNotFound();
                }
                // Create the view model
                var model = new EventViewModel();
                model.Id = events.Id;
                model.Title = events.Title;
                model.AdditionalContent = events.AdditionalContent;
                model.Place = events.Place;
                model.Time = events.Time;
                model.Date = events.Date;
                // Pass the view model to view 
                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(EventViewModel model)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                using (var database = new BlogDbContext())
                {
                    // Get events from database
                    var events = database.Events.FirstOrDefault(a => a.Id == model.Id);
                    // Set event properties
                    events.Id = model.Id;
                    events.Title = model.Title;
                    events.AdditionalContent = model.AdditionalContent;
                    events.Place = model.Place;
                    events.Time = model.Time;
                    events.Date = model.Date;
                    // Save event state in database
                    database.Entry(events).State = EntityState.Modified;
                    database.SaveChanges();
                    // Redirect to page
                    return RedirectToAction("List", "Event");
                }
            }
            // If model state is invalid, return the same view

            return View(model);
        }
    }
}