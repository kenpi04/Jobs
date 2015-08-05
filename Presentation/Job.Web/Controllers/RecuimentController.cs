using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job.Web.Controllers
{
    public class RecuimentController : Controller
    {
        public RecuimentController()
        {

        }
        //
        // GET: /Recuiment/
        public ActionResult Index()
        {
            return View();
        }
	}
}