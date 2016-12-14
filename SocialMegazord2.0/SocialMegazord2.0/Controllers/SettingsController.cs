using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialZord_project.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult ChangeImage()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ChangeUsername()
        {
            return View();
        }
    }
}