﻿using RAS.TimeSheets.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAS.TimeSheets.MVC
{
    public class HoliDayJson
    {

        public Guid ID { get; set; }
        
        public string Date { get; set; }
       
        public Guid CalendarID { get; set; }
    }
}