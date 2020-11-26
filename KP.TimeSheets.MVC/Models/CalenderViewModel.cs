using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAS.TimeSheets.MVC
{
    public class CalenderViewModel
    {
        public CalenderViewModel()
        {
            Holidays = new List<string>();
        }

        public string Title { get; set; }

        public bool IsSaturdayWD { get; set; }

        public bool IsSundayWD { get; set; }
   
        public bool IsMondayWD { get; set; }

        public bool IsTuesdayWD { get; set; }
    
        public bool IsWednesdayWD { get; set; }

        public bool IsThursdayWD { get; set; }
    
        public bool IsFridayWD { get; set; }

        public List<string> Holidays { get; set; }
    }
}