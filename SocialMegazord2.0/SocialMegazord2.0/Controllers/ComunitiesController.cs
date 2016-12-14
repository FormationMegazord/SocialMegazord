using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialZord_project.Controllers
{
    public class ComunitiesController : Controller
    {
        // GET: Comunities
        public ActionResult Entertainment()
        {
            return View();
        }

        public ActionResult Science()
        {
            return View();
        }

        public ActionResult MutualHelp()
        {
            return View();
        }
    }
}