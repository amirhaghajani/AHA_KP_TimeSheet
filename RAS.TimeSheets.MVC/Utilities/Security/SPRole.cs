using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Routing;

namespace RAS.TimeSheets.MVC
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class SPRole : AuthorizeAttribute
    {
        public string AccessDeniedController { get; set; }
        public string AccessDeniedAction { get; set; }

        public SPRole(string RoleNames)
        {
            string[] roleNames = RoleNames.Split(',');
            List<string> result = new List<string>();
            foreach (string tmpRoleName in roleNames)
            {
                switch (tmpRoleName)
                {
                    case "ViewSPDReports":
                        result.Add(ConfigurationManager.AppSettings["ViewSPDReports"].ToString());
                        break;
                    case "ViewDPDReports":
                        result.Add(ConfigurationManager.AppSettings["ViewDPDReports"].ToString());
                        break;
                    case "ViewBDAReports":
                        result.Add(ConfigurationManager.AppSettings["ViewBDAReports"].ToString());
                        break;
                    case "ViewITAReports":
                        result.Add(ConfigurationManager.AppSettings["ViewITAReports"].ToString());
                        break;
                    case "ViewCPOReports":
                        result.Add(ConfigurationManager.AppSettings["ViewCPOReports"].ToString());
                        break;
                    case "ViewSPAReports":
                        result.Add(ConfigurationManager.AppSettings["ViewSPAReports"].ToString());
                        break;
                    case "ViewONDReports":
                        result.Add(ConfigurationManager.AppSettings["ViewONDReports"].ToString());
                        break;
                    case "ViewMRDReports":
                        result.Add(ConfigurationManager.AppSettings["ViewMRDReports"].ToString());
                        break;
                    case "ViewOrgReports":
                        result.Add(ConfigurationManager.AppSettings["ViewOrgReports"].ToString());
                        break;
                    case "ViewPortfolioReports":
                        result.Add(ConfigurationManager.AppSettings["ViewPortfolioReports"].ToString());
                        break;
                    case "ViewAllReports":
                        result.Add(ConfigurationManager.AppSettings["ViewAllReports"].ToString());
                        break;
                }
            }
            this.Roles = string.Join(", ", result.ToArray());
        }

        

        public override void OnAuthorization(HttpActionContext actionContext)
        {
           
            //base.OnAuthorization(actionContext);
            if (!this.IsAuthorized(actionContext))
            {
                if (String.IsNullOrWhiteSpace(AccessDeniedController) || String.IsNullOrWhiteSpace(AccessDeniedAction))
                {
                    AccessDeniedController = "Home";
                    AccessDeniedAction = "AccessDenied";
                }
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                // RedirectToRouteResult(new RouteValueDictionary(new { Controller = AccessDeniedController, Action = AccessDeniedAction }));
            }
        }


    }
}