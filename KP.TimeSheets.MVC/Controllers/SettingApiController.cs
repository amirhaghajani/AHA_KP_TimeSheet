using KP.TimeSheets.Domain;
using KP.TimeSheets.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KP.TimeSheets.MVC.Controllers
{
    public class SettingApiController : ApiController
    {

        [HttpPost]
        public List<string> SaveOrganisationUnit(OrganisationUnitJson ou)
        {
            UnitOfWork uow = new UnitOfWork();
            List<string> result = new List<string>();
            OrgUnitManager om = new OrgUnitManager(uow);
            om.Save(new OrganisationUnitAssembler().ToEntity(ou));
            return result;
        }

        [HttpGet]
        public IEnumerable<OrganisationUnitJson> GetAllOrganisationUnits()
        {
            UnitOfWork uow = new UnitOfWork();
            OrgUnitManager organManager = new OrgUnitManager(uow);
            var s = organManager.GetAll();
            return new OrganisationUnitAssembler().ToJsons(s);
        }

        [HttpGet]
        public OrganisationUnitJson BuildNewOrganUnit()
        {
            return new OrganisationUnitJson()
            {
                ID = Guid.NewGuid(),
                ManagerFullName = "",
                ManagerID = Guid.Empty,
                Title = "",
                Users = new List<UserJson>()
            };

        }

        // GET: api/SettingApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SettingApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SettingApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SettingApi/5
        public void Delete(int id)
        {
        }
    }
}
