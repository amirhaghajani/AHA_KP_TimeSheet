using RAS.TimeSheets.Domain;
using RAS.TimeSheets.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAS.TimeSheets.MVC
{
    public class DisplayPeriodUtilities
    {

        public static DisplayPeriod ConvertPeriodNumberDateJsonToDisplayPeriod(PeriodNumberDateJson json)
        {
           
            UnitOfWork uow = new UnitOfWork();
            DisplayPeriodManager dpm = new DisplayPeriodManager(uow);
            UserManager userManager = new UserManager(uow);
            DisplayPeriod result = new DisplayPeriod();
            var currUser = new UserHelper().GetCurrent();
            result = dpm.GetDisplayPeriod(currUser);
            result.IsWeekly = json.IsWeekly;
            result.NumOfDays = json.Days;
            result.StartDate = DateUtility.GetMiladiDate(json.Date);
            return result;
        }

    }
}