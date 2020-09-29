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
    public class TasksController : Controller
    {
       

        // GET: Tasks
        public ActionResult Index()
        {
            UnitOfWork uow = new UnitOfWork();
            TaskManager taskManager = new TaskManager(uow);
            var currentUser = new UserHelper().GetCurrent();

            var tasks = taskManager.GetAll();
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(Guid id)
        {
            UnitOfWork uow = new UnitOfWork();
            TaskManager taskManager = new TaskManager(uow);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = taskManager.GetByID(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            UnitOfWork uow = new UnitOfWork();
            TaskManager taskManager = new TaskManager(uow);
            ProjectManager projectManager = new ProjectManager(uow);
            var currentUser = new UserHelper().GetCurrent();
            ViewBag.ParentTaskID = new SelectList(taskManager.GetAll(), "ID", "Title");
            ViewBag.ProjectID = new SelectList(projectManager.GetAll(), "ID", "Title");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ProjectID,ParentTaskID")] Task task)
        {
            UnitOfWork uow = new UnitOfWork();
            TaskManager taskManager = new TaskManager(uow);
            ProjectManager projectManager = new ProjectManager(uow);
            var currentUser = new UserHelper().GetCurrent();
            if (ModelState.IsValid)
            {
               
                task.ID = Guid.NewGuid();
                taskManager.AddTask(task);
                return RedirectToAction("Index");
            }

            ViewBag.ParentTaskID = new SelectList(taskManager.GetAll(), "ID", "Title", task.ParentTaskID);
            ViewBag.ProjectID = new SelectList(projectManager.GetAll(), "ID", "Title", task.ProjectID);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(Guid id)
        {

            UnitOfWork uow = new UnitOfWork();
            TaskManager taskManager = new TaskManager(uow);
            ProjectManager projectManager = new ProjectManager(uow);
            var currentUser = new UserHelper().GetCurrent();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = taskManager.GetByID(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentTaskID = new SelectList(taskManager.GetAll(), "ID", "Title", task.ParentTaskID);
            ViewBag.ProjectID = new SelectList(projectManager.GetAll(), "ID", "Title", task.ProjectID);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ProjectID,ParentTaskID")] Task task)
        {
            UnitOfWork uow = new UnitOfWork();
            TaskManager taskManager = new TaskManager(uow);
            ProjectManager projectManager = new ProjectManager(uow);
            var currentUser = new UserHelper().GetCurrent();

            if (ModelState.IsValid)
            {
                taskManager.EditTask(task);
                return RedirectToAction("Index");
            }
            ViewBag.ParentTaskID = new SelectList(taskManager.GetAll(), "ID", "Title", task.ParentTaskID);
            ViewBag.ProjectID = new SelectList(projectManager.GetAll(), "ID", "Title", task.ProjectID);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(Guid id)
        {
            UnitOfWork uow = new UnitOfWork();
            TaskManager taskManager = new TaskManager(uow);
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = taskManager.GetByID(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UnitOfWork uow = new UnitOfWork();
            TaskManager taskManager = new TaskManager(uow);
            ProjectManager projectManager = new ProjectManager(uow);
            var currentUser = new UserHelper().GetCurrent();
            taskManager.DeleteById(id);
            uow.SaveChanges();
            
            return RedirectToAction("Index");
        }

       
    }
}
