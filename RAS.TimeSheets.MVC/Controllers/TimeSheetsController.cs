using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAS.TimeSheets.MVC.Controllers
{
    public class TimeSheetsController : Controller
    {
        
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Confirmation()
        {
            return View();
        }

    }
}