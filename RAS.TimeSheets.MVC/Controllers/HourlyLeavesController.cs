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
    public class HourlyLeavesController : Controller
    {
        UnitOfWork UOW = new UnitOfWork();

        // GET: HourlyLeaves
        public ActionResult Index()
        {
            HourlyLeaveManager hl = new HourlyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var hourlyLeaves = hl.GetAllByUserID(currentUser.ID);
            return View(new HourlyLeaveAssembler().ToJsons(hourlyLeaves.ToList()));


        }

        // GET: HourlyLeaves/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourlyLeaveManager hl = new HourlyLeaveManager(UOW);
            HourlyLeave hourlyLeave = hl.GetByID(id);
            if (hourlyLeave == null)
            {
                return HttpNotFound();
            }
            return View(new HourlyLeaveAssembler().ToJson(hourlyLeave));
        }

        // GET: HourlyLeaves/Create
        public ActionResult Create()
        {
           
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");
            return View();
        }

        // POST: HourlyLeaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,From,To,ProjectID,PersianTimeFrom,PersianTimeTo,PersianLeaveDate")] HourlyLeave hourlyLeave)
        {
            HourlyLeaveManager hm = new HourlyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            hourlyLeave.UserId = currentUser.ID;
            hourlyLeave.OrganisationId = currentUser.OrganizationUnitID;

            var firstError = ModelState.Values.SelectMany(v => v.Errors).ToList();

            if (ModelState.IsValid)
            {
                hm.Add(hourlyLeave);
                return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");
          
            return View(hourlyLeave);
        }

        // GET: HourlyLeaves/Edit/5
        public ActionResult Edit(Guid id)
        {
            HourlyLeaveManager hm = new HourlyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourlyLeave hourlyLeave = hm.GetByID(id);
            if (hourlyLeave == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");
            return View(hourlyLeave);
        }

        // POST: HourlyLeaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjectID,PersianTimeFrom,PersianTimeTo,PersianLeaveDate")] HourlyLeave hourlyLeave)
        {
            HourlyLeaveManager hm = new HourlyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (ModelState.IsValid)
            {
                hm.Edit(hourlyLeave);

                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");
            ViewBag.SuccessorID = new SelectList(um.GetAll(), "ID", "UserTitle");
            return View(hourlyLeave);
        }

        // GET: HourlyLeaves/Delete/5
        public ActionResult Delete(Guid id)
        {
            HourlyLeaveManager hm = new HourlyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourlyLeave hourlyLeave = hm.GetByID(id);
            if (hourlyLeave == null)
            {
                return HttpNotFound();
            }
            return View(new HourlyLeaveAssembler().ToJson(hourlyLeave));
        }

        // POST: HourlyLeaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            HourlyLeaveManager hm = new HourlyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            hm.DeleteByID(id);
            return RedirectToAction("Index");
        }



        // GET: DailyLeaves/ApproveIndex/5
        public ActionResult ApproveIndex()
        {
            HourlyLeaveManager dlm = new HourlyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var hourlyLeaves = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 3 );
            return View(new HourlyLeaveAssembler().ToJsons(hourlyLeaves.ToList()));
        }

        public ActionResult Approve(Guid id)
        {
            HourlyLeaveManager dlm = new HourlyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourlyLeave hourlyLeave = dlm.GetByID(id);
            if (hourlyLeave == null)
            {
                return HttpNotFound();
            }
            return View(new HourlyLeaveAssembler().ToJson(hourlyLeave));
        }

        // POST: DailyLeaves/Delete/5
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveConfirmed(Guid id)
        {
            HourlyLeaveManager dlm = new HourlyLeaveManager(UOW);
            var result = dlm.GetByID(id);
            var currentUser = new UserHelper().GetCurrent();
            dlm.Approve(result);
            UOW.SaveChanges();

            return RedirectToAction("ApproveIndex", new { ac = "Approve" });
        }
        
        // POST: DailyLeaves/Approve/5
        public ActionResult Deny(Guid id)
        {
            
            HourlyLeaveManager dlm = new HourlyLeaveManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourlyLeave hourlyLeave = dlm.GetByID(id);
            if (hourlyLeave == null)
            {
                return HttpNotFound();
            }
            return View(new HourlyLeaveAssembler().ToJson(hourlyLeave));
        }

        // POST: DailyLeaves/Delete/5
        [HttpPost, ActionName("Deny")]
        [ValidateAntiForgeryToken]
        public ActionResult DenyConfirmed(Guid id)
        {
            HourlyLeaveManager dlm = new HourlyLeaveManager(UOW);
            var result = dlm.GetByID(id);
            var currentUser = new UserHelper().GetCurrent();
            dlm.Deny(result);
            UOW.SaveChanges();
            return RedirectToAction("ApproveIndex", new { ac = "Deny" });
        }

        public ActionResult DenyAll()
        {
            HourlyLeaveManager dlm = new HourlyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var hourlyLeaves = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 3);
            foreach (var leave in hourlyLeaves)
            {
                dlm.Deny(leave);
            }
            UOW.SaveChanges();
            return RedirectToAction("ApproveIndex", new { ac = "DenyAll" });
        }

        public ActionResult ApproveAll()
        {
            HourlyLeaveManager dlm = new HourlyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var hourlyLeaves = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 3);
            foreach (var leave in hourlyLeaves)
            {
                dlm.Approve(leave);
            }
            UOW.SaveChanges();
            return RedirectToAction("ApproveIndex", new { ac = "ApproveAll" });
        }


        public ActionResult ShowDenied()
        {
            HourlyLeaveManager dlm = new HourlyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var HourlyLeaves = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order  == 1);
            return View(new HourlyLeaveAssembler().ToJsons(HourlyLeaves.ToList()));
        }
        public ActionResult ShowApproves()
        {
            HourlyLeaveManager dlm = new HourlyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var HourlyLeaves = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 4);
            return View(new HourlyLeaveAssembler().ToJsons(HourlyLeaves.ToList()));
        }

        public ActionResult Resend(Guid id)
        {
            HourlyLeaveManager dlm = new HourlyLeaveManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var HourlyLeave = dlm.GetByID(id);
            dlm.Resend(HourlyLeave);
            UOW.SaveChanges();
            return RedirectToAction("ShowDenied", new { ac = "Resend" });
        }

    }
}
