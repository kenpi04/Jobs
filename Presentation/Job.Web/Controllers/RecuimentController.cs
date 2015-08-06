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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Post()
        {
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
                string savePath = Server.MapPath("~/Content/Images/CV/" + fileName);
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
        
	}
}