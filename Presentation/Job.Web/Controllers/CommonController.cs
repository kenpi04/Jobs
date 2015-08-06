using Job.Services.Directory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job.Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly ICommonService _commonService;
        public CommonController(ICommonService commonService)
        {
            this._commonService = commonService;
        }
        //
        // GET: /Common/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadPicture(HttpPostedFileBase Filedata)
        {
            string PathImage=Server.MapPath("~/Content/Images");

           
            var fileName = "";
            if (Filedata != null)
            {
                // IE
                HttpPostedFileBase httpPostedFile = Filedata;
                if (httpPostedFile == null)
                    throw new ArgumentException("No file uploaded");
                string ext = Path.GetExtension(httpPostedFile.FileName);
                 fileName=Guid.NewGuid().ToString()+ext;
                httpPostedFile.SaveAs(Path.Combine(PathImage));


                return Json(new { 
                name=fileName,
                link="~/Content/Images/"+fileName
                });
            }
            return Json("");
        }
        [HttpGet]
        public JsonResult GetDistrict(int ProvinceID)
        {
            var data = _commonService.GetAllDistrictByStateId(ProvinceID).Select(x=>new{x.Id,x.Name});
            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}