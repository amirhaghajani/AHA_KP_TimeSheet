using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KP.TimeSheets.MVC
{
    public class SettingsController : Controller
    {

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult ProjectsCalendars()
        {
            return View();
        }

        public ActionResult Departments()
        {
            return View();
        }
    }
}
