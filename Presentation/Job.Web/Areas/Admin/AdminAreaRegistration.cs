﻿using System.Web.Mvc;

namespace Job.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", controller="home", id = UrlParameter.Optional },
                namespaces: new[] { "Job.Web.Areas.Admin.Controllers"}
            );
        }
    }
}