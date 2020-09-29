using RAS.TimeSheets.Domain;
using RAS.TimeSheets.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAS.TimeSheets.MVC
{
    public class UserHelper
    {

        public User GetCurrent()
        {
            UnitOfWork uow = new UnitOfWork();
            var userManager = new UserManager(uow);
            var UserName = HttpContext.Current.User.Identity.Name;
            string[] test = UserName.ToString().Split('\\');
            UserName = test[0] + @"\" + test[1];
            return userManager.GetByUserName(UserName);
        }
    }
}