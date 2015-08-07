using Job.Services.CareerNews;
using Job.Web.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Job.Web.Helper;
using Job.Services.WorkContext;

namespace Job.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IWorkContext _workContext;

        public NewsController(INewsService newsService, IWorkContext workContext)
        {
            this._newsService = newsService;
            this._workContext = workContext;

        }
        //
        // GET: /News/
        public ActionResult Index(int page=1)
        {
            var pageModel = _newsService.GetAllNews(page);
            var model=pageModel.Select(x => new NewsModel
            {
                Id = x.Id,
                Title = x.Title,
                Time=x.CreateDate.RelativeFormat(),
                ImgUrl = x.Image,
                StateName = x.StateProvince.Name,
                DistrictName = x.DistrictId.HasValue ? x.District.Name : "",
                ViewCount=x.ViewCount,
                PostBy=x.User.UserName,
                ShortDes=x.ShortDes
            });
            return View(new PagedList.StaticPagedList<NewsModel>(model,page,pageModel.PageSize,pageModel.TotalItemCount));
        }
        
        public ActionResult NewsHome(int pageSize = 4)
        {
            var model = _newsService.GetAllNews(pageSize: pageSize).Select(x => new NewsModel
            {
                Id = x.Id,
                Title = x.Title,
               Time=x.CreateDate.RelativeFormat(),
                ImgUrl = x.Image,
                StateName = x.StateProvince.Name,
                DistrictName=x.DistrictId.HasValue?x.District.Name:""
            });
            return PartialView(model);
        }
        public ActionResult NewsDetail(int id)
        {
            var news = _newsService.GetById(id);
            if (news == null)
                return RedirectToRoute("HomePage");
            news.ViewCount++;
            var model =new NewsModel
            {
                Id = news.Id,
                Title = news.Title,
                Time = news.CreateDate.RelativeFormat(),
                ImgUrl = news.Image,
                StateName = news.StateProvince.Name,
                DistrictName = news.DistrictId.HasValue ? news.District.Name : "",
                ViewCount = news.ViewCount,
                PostBy = news.User.UserName,
                ShortDes = news.FullDescription
            };
            _newsService.Update(news);
            return View(model);
        }
	}
}