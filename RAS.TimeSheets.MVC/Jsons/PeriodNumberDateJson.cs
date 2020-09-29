using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAS.TimeSheets.MVC
{
    public class PeriodNumberDateJson
    {
        public string Date { get; set; }
        public int Days { get; set; }
        public bool  IsWeekly { get; set; }
        public string UserId { get; set; }
    }
}