using KP.TimeSheets.Domain;
using KP.TimeSheets.Persistance;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using Threading = System.Threading;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace KP.TimeSheets.MVC
{
    public class ProjectsAPIController : ApiController
    {

        #region HttpGet Methods

        [HttpGet]
        public IEnumerable<ProjectJson> GetProjects()
        {
           
            
                UnitOfWork uow = new UnitOfWork();
                UserManager userManager = new UserManager(uow);
                ProjectManager projectManager = new ProjectManager(uow);
                User currUser = new UserHelper().GetCurrent();
            var projectwithoutentities =   projectManager.GetByUser(currUser).ToList();
            List<Project> completeprojects = new List<Project>();

            foreach (var item in projectwithoutentities)
            {
              completeprojects.Add(projectManager.GetByID(item.ID));
            }
                //SyncWithPWA(uow);

           
            return completeprojects.ToJsons();
        }

        [HttpGet]
        public IEnumerable<ProjectJson> GetAllProjects()
        {
            IEnumerable<ProjectJson> result = null;
                UnitOfWork uow = new UnitOfWork();
              
                ProjectManager projectManager = new ProjectManager(uow);
                

                //SyncWithPWA(uow);
                result = projectManager.GetAll().ToJsons();
           
           
            return result;
        }

        #endregion

        #region HttpPost Methods

        [HttpPost]
        public IEnumerable<TaskJson> GetTasks(JObject jsonObject)
        {
            IEnumerable<TaskJson> result = null;
            try
            {
                dynamic projectJson = jsonObject;
                UnitOfWork uow = new UnitOfWork();
              
                ProjectManager projectManager = new ProjectManager(uow);
                TaskManager taskManager = new TaskManager(uow);
                User currUser = new UserHelper().GetCurrent();
                //SyncWithPWA(uow);
                Project project = projectManager.GetByID(Guid.Parse(projectJson.ID.ToString()));
                result = taskManager.GetByProject(project, currUser).ToJsons();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        #endregion

        #region Private Methods

        private void SyncWithPWA(UnitOfWork unitOfWork)
        {
            try
            {
                ProjectManager projectManager = new Domain.ProjectManager(unitOfWork);
                projectManager.Sync();
            }
            catch(Exception ex)
            {
                string str = ex.Message;
            }
        }


        #endregion

    }
}
