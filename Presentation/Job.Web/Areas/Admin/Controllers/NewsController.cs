using Job.Core.Domain;
using Job.Services.WorkContext;
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
        private readonly IWorkContext _workContext;
        public NewsController(INewsService newsService, IWorkContext workContext)
        {
            _newsService = newsService;
            _workContext=workContext;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int pageindex=1,int pageSize=20)
        {
            var data = _newsService.GetAllNews(pageindex, pageSize);
            return View(data);
        }
        public ActionResult Create(int id=0)
        {
            var news=new News();
            if(id>0)
            {
              news=_newsService.GetById(id);
            if(news==null)
                return RedirectToAction("List");
            }
            return View(news);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(News model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    model.UpdateDate = DateTime.Now;
                    model.ViewCount = 0;
                    model.CreateBy = _workContext.CurrentUser.Id;
                    model.CreateDate = DateTime.Now;
                    _newsService.Insert(model);

                }
                else
                {
                    var news = _newsService.GetById(model.Id);
                    if (news != null)
                    {
                        news.Title = model.Title;
                        news.FullDescription = model.FullDescription;
                        news.Image = model.Image;
                        news.UpdateDate = DateTime.Now;
                        _newsService.Update(news);
                    }
                }
                return RedirectToAction("list");
            }
            ViewBag.Error = "Thêm không thành công";
            return View(model);
        }
	}
}