using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Job.Core.Domain;
using Job.Services.Recuiments;
using Job.Services.Directory;
using System.IO;
using System.Threading.Tasks;
using Job.Services.CareerNews;
using Job.Web.Models.News;
using Job.Web.Models.CareerNews;

namespace Job.Web.Controllers
{
    public class RecuimentController : Controller
    {
        private readonly IRecuimentService _recuimentService;
        private readonly ICommonService _commonService;
        private readonly ICareerNewsService _careerNewService;
     
        public RecuimentController( IRecuimentService recuimentService,
     ICommonService commonService,
      ICareerNewsService careerNewService)
        {
            this._recuimentService = recuimentService;
            this._commonService = commonService;
            this._careerNewService = careerNewService;
        }
        //
        // GET: /Recuiment/
        public ActionResult Index(int page=1)
        {
        

            return View();
        }
        public ActionResult Detail(int id)
        {
            var item = _careerNewService.GetById(id);
            if (item == null)
                return RedirectToRoute("HomePage");
         //   if (item.CareerNewsShop.FirstOrDefault()!=null)
               //  ViewBag.LocationId = item.CareerNewsShop.FirstOrDefault().Shop.LocationId;
         //   if(item.CareerNewsShop.)
            return View(item);

        }
        public ActionResult RelativeItems(int groupId,int stateId)
        {
            var list = _careerNewService.GetAll(groupid: groupId, onlyHaveQuantity: true,stateId:stateId).Select(x => PrepairingCareerNews(x));
            if (list.Count() == 0)
                return Content("");
            return View(list);
        }
        public ActionResult CareerListGroup(int groupId)
        {
            var list = _careerNewService.GetAll(groupid: groupId,onlyHaveQuantity:true).Select(x => PrepairingCareerNews(x)).ToList();
            return View(list);
        }
        public ActionResult Post(int id=0)
        {
            ViewBag.CateId = id;
            var model = new Recuitment();
            ViewBag.CateList = _careerNewService.GetAllCareerNewsCate().ToDictionary(x => x.Id, x => x.Name);
            ViewBag.StateList = _commonService.GetAllStateProvince().ToDictionary(x => x.Id, x => x.Name);
            return View(model);
        }
        [HttpPost]
        public ActionResult Post(Recuitment model)
        {
         
           if(ModelState.IsValid)
           {
               model.DateCreate = DateTime.Now;
               _recuimentService.Insert(model);
               return Json("Ok");
           }
           return Json("Notok");

        }
        [HttpPost]
        public string UploadFile()
        {
            var files = Request.Files;
            if(files.Count>0)
            {
                var file = files[0];
                string fileName = string.Format("{0}_{1}", DateTime.Now.ToString("ddMMyyyyhhmmss"), file.FileName);
                string savePath = Server.MapPath("~/Content/CV/" + fileName);
                file.SaveAs(savePath);
                return fileName;
            }
            return "";

        }
        [HttpPost]
        public async Task<string> CaptureSave()
        {
            using (var stream = Request.InputStream)
            {
                byte[] bytes = new byte[1024];
                await stream.WriteAsync(bytes, 0, (int)stream.Length);
                return Convert.ToBase64String(bytes);
            }
          
            
        }
        public ActionResult SlideBox(int pageSize=10)
        {
            var item = _careerNewService.GetAll(pageSize: pageSize,includePriotyBox:true,onlyHaveQuantity:true).Select(x => PrepairingCareerNews(x)).ToList();
            return View(item);
        }

       
        public ActionResult CareerNewsHome(int pageSize=3)
        {
            var model = _careerNewService.GetAll(pageSize: pageSize).Select(x => PrepairingCareerNews(x)).ToList();
            return View(model);
        }
        private CareerNewsModel PrepairingCareerNews(CareerNews entity)
        {
            return new CareerNewsModel
            {
                Id = entity.Id,
                StateList = string.Join(",", entity.CareerNewsShop.Select(y => y.Shop.District.StateProvice.Name).ToList()),
                CateGroupName = entity.CareerNewCate.GetGroup(),
                CateName = entity.CareerNewCate.Name,
                ImgUrl = entity.Image,
                Quantity = entity.CareerNewsShop.Sum(y => y.Quantity),
            };
        }
        
	}
}