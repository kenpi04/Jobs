using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job.Web.Controllers
{
    public class NewsController : Controller
    {
        public NewsController()
        {

        }
        //
        // GET: /News/
        public ActionResult Index()
        {
            return View();
        }
	}
}