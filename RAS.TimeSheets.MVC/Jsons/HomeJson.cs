using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAS.TimeSheets.MVC
{
    public class HomeJson
    {
        public AllEntityJson Yesterday { get; set; }
        public AllEntityJson ThisMonth { get; set; }


    }
}