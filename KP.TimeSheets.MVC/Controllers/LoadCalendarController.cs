using KP.TimeSheets.Domain;
using KP.TimeSheets.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace KP.TimeSheets.MVC
{
    public class LoadCalendarController : ApiController
    {
        [HttpGet]
        public IEnumerable<CalendarJson> GetCalendarItems()
        {
            var manager = new CalendarManager(new UnitOfWork());
            var calenders = manager.GetAll();
            var result = CalendarAssembler.ToJsons(calenders);
            return result;
        }


        [HttpGet]
        public IEnumerable<UserJson> GetAllUsers()
        {
            var manager = new UserManager(new UnitOfWork());
            var Users = manager.GetAll();
            var result = new UserAssembler().ToJsons(Users);
            return result;
        }

        [HttpGet]
        public string DeleteCalendarItem(Guid calendarID)
        {
            var manager = new CalendarManager(new UnitOfWork());
            manager.Remove(calendarID);
            return "آیتم انتخاب شده با موفقیت حذف شد";
        }

        [HttpGet]
        public CalendarJson BuildCalendar()
        {
            var manager = new CalendarManager(new UnitOfWork());
            var calender = manager.BuildCalendar();
  
            var json=CalendarAssembler.ToJson(calender);
            return json;

            
        }

        [HttpPost]
        public string SaveCalendar(CalendarJson json)
        {
            var manager = new CalendarManager(new UnitOfWork());
            var calendar = CalendarAssembler.ToCalender(json);
            manager.SaveCalendar(calendar);
            return "بروزرسانی با موفقیت انجام شد.";
        }

        [HttpPost]
        public string AssignCalendarAndManager(AssignManagerAndCalendarToProjectJson json)
        {
            var manager = new ProjectManager(new UnitOfWork());
            var project = manager.GetByID(Guid.Parse(json.ProjectId));
            project.CalendarID = Guid.Parse(json.CalendarId);
            project.OwnerID = Guid.Parse(json.UserId);
            manager.Save(project);
            return "بروزرسانی با موفقیت انجام شد.";
        }
        

    }
}
