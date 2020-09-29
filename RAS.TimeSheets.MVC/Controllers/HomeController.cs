using RAS.TimeSheets.Domain;
using RAS.TimeSheets.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAS.TimeSheets.MVC
{
  
    public class HomeController : Controller
    {

        #region Public Methods 
         public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            HttpCookie cookie = Request.Cookies["TSWA-Last-User"];
            cookie = new HttpCookie("TSWA-Last-User", string.Empty)
            {
                Expires = DateTime.Now.AddYears(-5)
            };

            Response.Cookies.Set(cookie);

            base.Session.Abandon();
            return View();
        }


        [AllowAnonymous]
        public ActionResult LoginOtherUser()
        {
            HttpCookie cookie = Request.Cookies["TSWA-Last-User"];

            if (User.Identity.IsAuthenticated == false || cookie == null || StringComparer.OrdinalIgnoreCase.Equals(User.Identity.Name, cookie.Value))
            {
                string name = string.Empty;

                if (Request.IsAuthenticated)
                {
                    name = User.Identity.Name;
                }

                cookie = new HttpCookie("TSWA-Last-User", name);
                Response.Cookies.Set(cookie);

                Response.AppendHeader("Connection", "close");
                Response.StatusCode = 0x191;
                Response.Clear();
                Response.Write("Unauthorized. Reload the page to try again...");
                Response.End();
                return RedirectToAction("Index");
            }
            cookie = new HttpCookie("TSWA-Last-User", string.Empty)
            {
                Expires = DateTime.Now.AddYears(-5)
            };
            Response.Cookies.Set(cookie);
            return RedirectToAction("Index");
        }

        public ActionResult Sync()
        {
            UnitOfWork uow = new UnitOfWork();
            uow.ProjectRepository.Sync();
            uow.SaveChanges();
            return View();
        }


        #endregion
    }
}