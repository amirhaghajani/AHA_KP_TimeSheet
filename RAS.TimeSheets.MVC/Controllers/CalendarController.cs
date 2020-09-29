using RAS.TimeSheets.Domain;
using RAS.TimeSheets.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RAS.TimeSheets.MVC
{
    public class CalendarController : ApiController
    {

        #region Public Api Methods
        [HttpPost]
        public List<Calendar> GetAll()
        {
            UnitOfWork uow = new UnitOfWork();
            var calendarManager = new CalendarManager(uow);
            return calendarManager.GetAll().ToList();
        }

        #endregion
    }
}
