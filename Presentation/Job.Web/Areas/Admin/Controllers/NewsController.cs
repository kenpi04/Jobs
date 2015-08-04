using Job.Core.Domain;
using Job.Services.CareerNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        //
        // GET: /Admin/News/
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int pageindex,int pageSize)
        {
            var data = _newsService.GetAllNew(pageindex, pageSize);
            return View(data);
        }
        public ActionResult Create()
        {

            return View();
        }
        public ActionResult Edit()
        {
            return View();

        }
        [HttpPost]
        public ActionResult CreateUpdate(News model)
        {
            return View(model);
        }
	}
}