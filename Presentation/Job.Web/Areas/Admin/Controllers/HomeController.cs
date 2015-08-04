using Job.Services.CareerNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly INewsService _newService;
        public HomeController(INewsService newService)
        {
            _newService = newService;
        }
        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}