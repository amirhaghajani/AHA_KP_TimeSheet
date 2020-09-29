using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RAS.TimeSheets.Domain;
using RAS.TimeSheets.Persistance;

namespace RAS.TimeSheets.MVC.Controllers
{
    public class DailyLeavesController : Controller
    {
        UnitOfWork UOW = new UnitOfWork();

       

        // GET: DailyLeaves
        public ActionResult Index()
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var dailyLeaves = dlm.GetAllByUserID(currentUser.ID);
            return View(new DailyLeaveAssembles().ToJsons(dailyLeaves.ToList()));
        }

        // GET: DailyLeaves/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            DailyLeave dailyLeave = dlm.GetByID(id);
            if (dailyLeave == null)
            {
                return HttpNotFound();
            }
            return View(new DailyLeaveAssembles().ToJson(dailyLeave));
        }

        // GET: DailyLeaves/Create
        public ActionResult Create()
        {
          
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");
            ViewBag.SuccessorID = new SelectList(um.GetAll(), "ID", "UserTitle");
            return View();
        }

        // POST: DailyLeaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,From,To,PersianDateFrom,PersianDateTo,SuccessorID,ProjectID,Type")] DailyLeave dailyLeave)
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            dailyLeave.UserID = currentUser.ID;
            dailyLeave.OrganisationId = currentUser.OrganizationUnitID;

            if (ModelState.IsValid)
            {
                dlm.Add(dailyLeave);
                return RedirectToAction("Index");
            }
           
          
            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");
            ViewBag.SuccessorID = new SelectList(um.GetAll(), "ID", "UserTitle");
            return View(dailyLeave);
        }

        // GET: DailyLeaves/Edit/5
        public ActionResult Edit(Guid id)
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyLeave dailyLeave = dlm.GetByID(id);
            if (dailyLeave == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");
            ViewBag.SuccessorID = new SelectList(um.GetAll(), "ID", "UserTitle");
            return View(dailyLeave);
        }

        // POST: DailyLeaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,From,To,SuccessorID,ProjectID,Type")] DailyLeave dailyLeave)
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (ModelState.IsValid)
            {
                dlm.Edit(dailyLeave);
             
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");
            ViewBag.SuccessorID = new SelectList(um.GetAll(), "ID", "UserTitle");
            return View(dailyLeave);
        }

        // GET: DailyLeaves/Delete/5
        public ActionResult Delete(Guid id)
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyLeave dailyLeave =dlm.GetByID(id);
            if (dailyLeave == null)
            {
                return HttpNotFound();
            }
            return View(new DailyLeaveAssembles().ToJson(dailyLeave));
        }

        // POST: DailyLeaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            dlm.DeleteByID(id);
            return RedirectToAction("Index");
        }

        // GET: DailyLeaves/ApproveIndex/5
        public ActionResult ApproveIndex()
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var dailyLeaves = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x=>x.WorkflowStage.Order ==3);
            return View(new DailyLeaveAssembles().ToJsons(dailyLeaves.ToList()));
        }



        public ActionResult Approve(Guid id)
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyLeave dailyLeave = dlm.GetByID(id);
            if (dailyLeave == null)
            {
                return HttpNotFound();
            }
            return View(new DailyLeaveAssembles().ToJson(dailyLeave));
        }

        // POST: DailyLeaves/Delete/5
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveConfirmed(Guid id)
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            var result = dlm.GetByID(id);
            var currentUser = new UserHelper().GetCurrent();
            dlm.Approve(result);
            UOW.SaveChanges();

            return RedirectToAction("ApproveIndex", new { ac = "Approve" });
        }



        // POST: DailyLeaves/Approve/5




        public ActionResult Deny(Guid id)
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyLeave dailyLeave = dlm.GetByID(id);
            if (dailyLeave == null)
            {
                return HttpNotFound();
            }
            return View(new DailyLeaveAssembles().ToJson(dailyLeave));
        }

        // POST: DailyLeaves/Delete/5
        [HttpPost, ActionName("Deny")]
        [ValidateAntiForgeryToken]
        public ActionResult DenyConfirmed(Guid id)
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            var result = dlm.GetByID(id);
            var currentUser = new UserHelper().GetCurrent();
            dlm.Deny(result);
            UOW.SaveChanges();
            return RedirectToAction("ApproveIndex", new { ac = "Deny" });
        }

        public ActionResult DenyAll()
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var dailyLeaves = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 3);
            foreach (var leave in dailyLeaves)
            {
                dlm.Deny(leave);
            }
            UOW.SaveChanges();
            return RedirectToAction("ApproveIndex", new { ac = "DenyAll" });
        }

        public ActionResult ApproveAll()
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var dailyLeaves = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order ==3);
            foreach (var leave in dailyLeaves)
            {
                dlm.Approve(leave);
            }
            UOW.SaveChanges();
            return RedirectToAction("ApproveIndex", new { ac = "ApproveAll" });
        }

        public ActionResult ShowDenied()
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var dailyLeaves = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 1);
            return View(new DailyLeaveAssembles().ToJsons(dailyLeaves.ToList()));
        }
        public ActionResult ShowApproves()
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var dailyLeaves = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 4);
            return View(new DailyLeaveAssembles().ToJsons(dailyLeaves.ToList()));
        }

        public ActionResult Resend(Guid id )
        {
            DailyLeaveManager dlm = new DailyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var dailyLeave = dlm.GetByID(id);
                dlm.Resend(dailyLeave);
            UOW.SaveChanges();
            return RedirectToAction("ShowDenied", new { ac = "Resend" });
        }



    }
}
