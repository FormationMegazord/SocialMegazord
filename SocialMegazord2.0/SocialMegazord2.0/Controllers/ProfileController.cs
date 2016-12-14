using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialZord_project.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Interests()
        {
            return View();
        }

        public ActionResult myevents()
        {
            return View();
        }

        public ActionResult myvideos()
        {
            return View();
        }
    }
}