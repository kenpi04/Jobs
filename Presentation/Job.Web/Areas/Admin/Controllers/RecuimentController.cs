using Job.Services.Recuiments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job.Web.Areas.Admin.Controllers
{
    public class RecuimentController : Controller
    {
        private readonly IRecuimentService _recuimentService;
        private readonly ICareerNewService
        //
        // GET: /Admin/Recuiment/
        public ActionResult Index()
        {
            return View();
        }
	}
}