using KP.TimeSheets.Domain;
using KP.TimeSheets.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KP.TimeSheets.MVC.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult ReportView(FinalReport model)
        {
            var reports = Session["Reports"] as FinalReport;
            return View(reports);
        }

        //Bunch Of projects Report

        public ActionResult BreakingProjectsByMonth()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BreakingProjectsByMonth(ReportParametersFromToJson parameters)
        {
            if (ModelState.IsValid)
            {
            Session["Reports"] = new ReportAssembler().AssembleBreakingProjectsByMonth(parameters);
            return RedirectToAction("ReportView");
            }
            else
            {
                return View();
            }

        }


        // Single Project Report Monthly
        public ActionResult BreakingProjectByMonth()
        {

            UnitOfWork uow = new UnitOfWork();
            UserManager UserManager = new UserManager(uow);
            ProjectManager projectManager = new ProjectManager(uow);
            var currentUser = new UserHelper().GetCurrent();
            var t = projectManager.GetByUser(currentUser);
            ViewBag.ProjectIDs = projectManager.GetByUser(currentUser);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BreakingProjectByMonth(ReportParametrsFromToProjectIdJson parameters)
        {
            if (ModelState.IsValid)
            {
                Session["Reports"] = new ReportAssembler().AssembleBreakingProjectByMonth(parameters);
                return RedirectToAction("ReportView");
            }
            else
            {
                UnitOfWork uow = new UnitOfWork();
                UserManager UserManager = new UserManager(uow);
                ProjectManager projectManager = new ProjectManager(uow);
                var currentUser = new UserHelper().GetCurrent();
                var t = projectManager.GetByUser(currentUser);
                ViewBag.ProjectIDs = projectManager.GetByUser(currentUser);
                return View();

            }
        }



        public ActionResult PersonelsAndProjects()
        {
            UnitOfWork uow = new UnitOfWork();
            UserManager UserManager = new UserManager(uow);
            ProjectManager projectManager = new ProjectManager(uow);
            var currentUser = new UserHelper().GetCurrent();
            //ViewBag.UserID = new SelectList(UserManager.GetMyEmployees(currentUser), "ID", "UserTitle");
            //ViewBag.ProjectID = new SelectList(projectManager.GetByUser(currentUser), "ID", "Title");

            var users = new List<SelectListItem>();

            foreach (var user in UserManager.GetMyEmployees(currentUser).ToList())
            {
                var pushuser = new SelectListItem();
                pushuser.Text = user.UserTitle;
                pushuser.Value = user.ID.ToString();
                users.Add(pushuser);
            }
            ViewBag.Users = users;

            var projects = new List<SelectListItem>();
            foreach (var user in UserManager.GetMyEmployees(currentUser).ToList())
            {
                foreach (var project in projectManager.GetByUser(user).ToList())
                {
                    var pushproject = new SelectListItem();
                    pushproject.Text = project.Title;
                    pushproject.Value = project.ID.ToString();
                    projects.Add(pushproject);
                }
            }
            ViewBag.Projects = projects.GroupBy(x => x.Text).Select(y => y.FirstOrDefault()).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PersonelsAndProjects(ReportParametersJson parameters)
        {
            if (ModelState.IsValid)
            {
            Session["Reports"] = new ReportAssembler().AssemblePersonnelsAndProjects(parameters);
            return RedirectToAction("ReportView");
            }
            else
            {
                UnitOfWork uow = new UnitOfWork();
                UserManager UserManager = new UserManager(uow);
                ProjectManager projectManager = new ProjectManager(uow);
                var currentUser = new UserHelper().GetCurrent();
                //ViewBag.UserID = new SelectList(UserManager.GetMyEmployees(currentUser), "ID", "UserTitle");
                //ViewBag.ProjectID = new SelectList(projectManager.GetByUser(currentUser), "ID", "Title");

                var users = new List<SelectListItem>();

                foreach (var user in UserManager.GetMyEmployees(currentUser).ToList())
                {
                    var pushuser = new SelectListItem();
                    pushuser.Text = user.UserTitle;
                    pushuser.Value = user.ID.ToString();
                    users.Add(pushuser);
                }
                ViewBag.Users = users;

                var projects = new List<SelectListItem>();
                foreach (var user in UserManager.GetMyEmployees(currentUser).ToList())
                {
                    foreach (var project in projectManager.GetByUser(user).ToList())
                    {
                        var pushproject = new SelectListItem();
                        pushproject.Text = project.Title;
                        pushproject.Value = project.ID.ToString();
                        projects.Add(pushproject);
                    }
                }
                ViewBag.Projects = projects.GroupBy(x => x.Text).Select(y => y.FirstOrDefault()).ToList();
                return View();
            }
        }



        public ActionResult TotalWorkHoursOnProjects()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TotalWorkHoursOnProjects(ReportParametersFromToJson parameters)
        {
           
            if (ModelState.IsValid)
            {
                Session["Reports"] = new ReportAssembler().AssembleTotalWorkHoursOnProjects(parameters);
                return RedirectToAction("ReportView");
            }
            else
            {
                return View();
            }

        }


        public ActionResult DailyOnProjects()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DailyOnProjects(ReportParametersFromToJson parameters)
        {

            if (ModelState.IsValid)
            {
                Session["Reports"] = new ReportAssembler().AssembleDailyOnProjects(parameters);
                return RedirectToAction("ReportView");
            }
            else
            {
                return View();
            }

        }






    }
}