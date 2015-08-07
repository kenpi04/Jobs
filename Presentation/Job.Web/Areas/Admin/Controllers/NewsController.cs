using Job.Core.Domain;
using Job.Services.WorkContext;
using Job.Services.CareerNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Job.Services.Recuiments;
using Job.Services.Directory;
using System.Globalization;
using Job.Web.Helper;

namespace Job.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        //
        // GET: /Admin/News/
        private readonly INewsService _newsService;
        private readonly IWorkContext _workContext;
        private readonly ICareerNewsService _careerNewsService;
        private readonly ICommonService _commonService;
        public NewsController(INewsService newsService, IWorkContext workContext, ICareerNewsService careerNewsService, ICommonService commonService)
        {
            _newsService = newsService;
            _workContext = workContext;
            _careerNewsService = careerNewsService;
            _commonService = commonService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int pageindex = 1, int pageSize = 20)
        {
            var data = _newsService.GetAllNews(pageindex, pageSize);
            return View(data);
        }
        public ActionResult Create(int id = 0)
        {
            var news = new News();
            if (id > 0)
            {
                news = _newsService.GetById(id);
                if (news == null)
                    return RedirectToAction("List");
            }
            ViewBag.StateList = _commonService.GetAllStateProvince().ToDictionary(x => x.Id, x => x.Name);
            return View(news);
        }

        [HttpPost]

        public ActionResult Create(News model)
        {
            if (ModelState.IsValid)
            {
                model.ShortDes = model.FullDescription.StripTagsRegex();
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
        [HttpPost]
        public string DeleteNews(int id)
        {
            var item = _newsService.GetById(id);
            _newsService.Delete(item);
            return "Xóa thành công";
        }
        #region CareerNews
        public ActionResult CareerNewsList(int pageIndex = 1, int pageSize = 20)
        {
            var data = _careerNewsService.GetAll(pageIndex, pageSize);

            return View(data);
        }
        public ActionResult CreateCareerNews(int id = 0)
        {
            var news = new CareerNews();
            if (id > 0)
            {
                news = _careerNewsService.GetById(id);
                if (news == null)
                    return RedirectToAction("CareerNewsList");
            }
            ViewBag.CateList = _careerNewsService.GetAllCareerNewsCate().Select(x => new { x.Id, x.Name,x.GroupName });
            ViewBag.ShopList = _commonService.GetAllShop();
            return View(news);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateCareerNews(CareerNews model, FormCollection form)
        {
            var listShopId = new List<int>();
            var listEndDate = new List<DateTime>();
            var shops = form.GetValues("shops");
            listShopId = shops.Select(x => int.Parse(x)).ToList();


            if (model.Id == 0)
            {
                model.CreateBy = _workContext.CurrentUser.Id;
                model.CreateDate = DateTime.Now;
                model.Deleted = false;
                for (int i = 0; i < listShopId.Count; i++)
                {
                    int quantity = 0;
                    DateTime? endate=null;
                    string quantityFormValue = form["quantity_" + listShopId[i]];
                    if (quantityFormValue != null)
                    {
                        quantity = int.Parse(quantityFormValue);
                    }
                    string endDateString=form["date_end_"+listShopId[i]];
                    if (endDateString != null)
                        endate=DateTime.ParseExact(endDateString,"dd/MM/yyyy",CultureInfo.CurrentCulture);
                    var a = new CareerNews_Shop_Mapping
                    {

                        ShopId = listShopId[i],
                        Quantity=quantity,
                        EndDate=endate.Value,
                      

                    };
                    model.CareerNewsShop.Add(a);


                }
                _careerNewsService.Insert(model);
                ViewData["mgs"] = "Thêm thành công";
                return RedirectToAction("CreateCareerNews", new {id=model.Id });

            }
            else
            {
                var b = _careerNewsService.GetById(model.Id);
               
                b.FullDes = model.FullDes;
                b.Salary = model.Salary;
                
                var listShopDelete=b.CareerNewsShop.Where(x=>!listShopId.Contains(x.Id));
                foreach(var i in listShopDelete)
                {
                    b.CareerNewsShop.Remove(i);
                }
                foreach(var i in listShopId)
                {
                    string id=form["shop_news_"+i];
                    int quantity = 0;
                    DateTime? endate=null;
                    string quantityFormValue = form["quantity_" + i];
                    if (quantityFormValue != null)
                    {
                        quantity = int.Parse(quantityFormValue);
                    }
                    string endDateString=form["enddate_"+i];
                    if(quantityFormValue!=null)
                        endate=DateTime.ParseExact(endDateString,"dd/MM/yyyy",CultureInfo.CurrentCulture);
                    if(string.IsNullOrEmpty(id))
                    {
                        var a = new CareerNews_Shop_Mapping
                        {

                            ShopId = i,
                            Quantity =quantity,
                            EndDate = endate.Value,
                        };
                        b.CareerNewsShop.Add(a);
                    }
                    else
                    {
                        var a = b.CareerNewsShop.FirstOrDefault(x => x.Id == int.Parse(id));
                        if(a!=null)
                        {
                            a.Quantity = quantity;
                            a.EndDate = endate.Value;
                        }
                    }
                    
                }
                _careerNewsService.Update(b);
                ViewData["mgs"] = "Cập nhật thành công";
                return RedirectToAction("CreateCareerNews", new { id = model.Id });
            }
          
            return View(model);
        }

        [HttpPost]
        public string DeleteCareeeNews(int id)
        {
            var item = _careerNewsService.GetById(id);
            _careerNewsService.Delete(item);
            return "Xóa thành công";
        }
        #endregion
    }
}