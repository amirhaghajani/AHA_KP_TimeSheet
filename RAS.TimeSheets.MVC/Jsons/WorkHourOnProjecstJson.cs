using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAS.TimeSheets.MVC
{
    public class WorkHourOnProjecstJson
    {
        public Guid ProjectId { get; set; }
        public string   Title { get; set; }
        public string Hour { get; set; }

    }
}