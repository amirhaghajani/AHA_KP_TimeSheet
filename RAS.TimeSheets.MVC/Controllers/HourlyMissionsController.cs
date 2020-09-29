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
    public class HourlyMissionsController : Controller
    {
        UnitOfWork UOW = new UnitOfWork();

        // GET: HourlyMissions
        public ActionResult Index()
        {
            HourlyMissionManager hm = new HourlyMissionManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var HourlyMissions = hm.GetAllByUserID(currentUser.ID);
            return View(new HourlyMissionAssembler().ToJsons(HourlyMissions.ToList()));
        }

        // GET: HourlyMissions/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourlyMissionManager hm = new HourlyMissionManager(UOW);
            HourlyMission HourlyMission = hm.GetByID(id);
            if (HourlyMission == null)
            {
                return HttpNotFound();
            }
            return View(new HourlyMissionAssembler().ToJson(HourlyMission));
        }

        // GET: HourlyMissions/Create
        public ActionResult Create()
        {
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");
            return View();
        }

        // POST: HourlyMissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProjectID,PersianTimeFrom,PersianTimeTo,PersianMissionDate")] HourlyMission hourlyMission)
        {
            HourlyMissionManager hm = new HourlyMissionManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            hourlyMission.UserID = currentUser.ID;
            hourlyMission.OrganisationId = currentUser.OrganizationUnitID;

            var firstError = ModelState.Values.SelectMany(v => v.Errors).ToList();

            if (ModelState.IsValid)
            {
                hm.Add(hourlyMission);
                return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");

            return View(hourlyMission);
        }

        // GET: HourlyMissions/Edit/5
        public ActionResult Edit(Guid id)
        {
            HourlyMissionManager hm = new HourlyMissionManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourlyMission hourlyMission = hm.GetByID(id);
            if (hourlyMission == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");
            return View(hourlyMission);
        }

        // POST: HourlyMissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjectID,PersianTimeFrom,PersianTimeTo,PersianMissionDate")] HourlyMission hourlyMission)
        {
            HourlyMissionManager hm = new HourlyMissionManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (ModelState.IsValid)
            {
                hm.Edit(hourlyMission);

                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(pm.GetByUser(currentUser), "ID", "Title");
            ViewBag.SuccessorID = new SelectList(um.GetAll(), "ID", "UserTitle");
            return View(hourlyMission);
        }

        // GET: HourlyMissions/Delete/5
        public ActionResult Delete(Guid id)
        {
            HourlyMissionManager hm = new HourlyMissionManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourlyMission hourlyMission = hm.GetByID(id);
            if (hourlyMission == null)
            {
                return HttpNotFound();
            }
            return View(new HourlyMissionAssembler().ToJson(hourlyMission));
        }

        // POST: HourlyMissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            HourlyMissionManager hm = new HourlyMissionManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            hm.DeleteByID(id);
            return RedirectToAction("Index");
        }




        // GET: DailyMissions/ApproveIndex/5
        public ActionResult ApproveIndex()
        {
            HourlyMissionManager dlm = new HourlyMissionManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var hourlyMissions = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 3);
            return View(new HourlyMissionAssembler().ToJsons(hourlyMissions.ToList()));
        }

        public ActionResult Approve(Guid id)
        {
            HourlyMissionManager dlm = new HourlyMissionManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourlyMission hourlyMission = dlm.GetByID(id);
            if (hourlyMission == null)
            {
                return HttpNotFound();
            }
            return View(new HourlyMissionAssembler().ToJson(hourlyMission));
        }

        // POST: DailyMissions/Delete/5
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveConfirmed(Guid id)
        {
            HourlyMissionManager dlm = new HourlyMissionManager(UOW);
            var result = dlm.GetByID(id);
            var currentUser = new UserHelper().GetCurrent();
            dlm.Approve(result);
            UOW.SaveChanges();

            return RedirectToAction("ApproveIndex", new { ac = "Approve" });
        }

        // POST: DailyMissions/Approve/5
        public ActionResult Deny(Guid id)
        {

            HourlyMissionManager dlm = new HourlyMissionManager(UOW);
            ProjectManager pm = new ProjectManager(UOW);
            UserManager um = new UserManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HourlyMission hourlyMission = dlm.GetByID(id);
            if (hourlyMission == null)
            {
                return HttpNotFound();
            }
            return View(new HourlyMissionAssembler().ToJson(hourlyMission));
        }

        // POST: DailyMissions/Delete/5
        [HttpPost, ActionName("Deny")]
        [ValidateAntiForgeryToken]
        public ActionResult DenyConfirmed(Guid id)
        {
            HourlyMissionManager dlm = new HourlyMissionManager(UOW);
            var result = dlm.GetByID(id);
            var currentUser = new UserHelper().GetCurrent();
            dlm.Deny(result);
            UOW.SaveChanges();
            return RedirectToAction("ApproveIndex", new { ac = "Deny" });
        }

        public ActionResult DenyAll()
        {
            HourlyMissionManager dlm = new HourlyMissionManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var hourlyMissions = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 3);
            foreach (var Mission in hourlyMissions)
            {
                dlm.Deny(Mission);
            }
            UOW.SaveChanges();
            return RedirectToAction("ApproveIndex", new { ac = "DenyAll" });
        }

        public ActionResult ApproveAll()
        {
            HourlyMissionManager dlm = new HourlyMissionManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var hourlyMissions = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 3 );
            foreach (var Mission in hourlyMissions)
            {
                dlm.Approve(Mission);
            }
            UOW.SaveChanges();
            return RedirectToAction("ApproveIndex", new { ac = "ApproveAll" });
        }




        public ActionResult ShowDenied()
        {
            HourlyMissionManager dlm = new HourlyMissionManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var HourlyMissions = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 1);
            return View(new HourlyMissionAssembler().ToJsons(HourlyMissions.ToList()));
        }

        public ActionResult ShowApproves()
        {
            HourlyMissionManager dlm = new HourlyMissionManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var HourlyMissions = dlm.GetByOrganisationID(currentUser.OrganizationUnitID).
                Where(x => x.WorkflowStage.Order == 4);
            return View(new HourlyMissionAssembler().ToJsons(HourlyMissions.ToList()));
        }

        public ActionResult Resend(Guid id)
        {
            HourlyMissionManager dlm = new HourlyMissionManager(UOW);
            var currentUser = new UserHelper().GetCurrent();
            var HourlyMission = dlm.GetByID(id);
            dlm.Resend(HourlyMission);
            UOW.SaveChanges();
            return RedirectToAction("ShowDenied", new { ac = "Resend" });
        }

    }
}
