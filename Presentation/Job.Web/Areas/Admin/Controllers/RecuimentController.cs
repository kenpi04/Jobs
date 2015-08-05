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
        public RecuimentController(IRecuimentService _recuimentService)
        {
            this._recuimentService = _recuimentService;
        }
        //
        // GET: /Admin/Recuiment/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int cateId=0,int locationId=0, int pageIndex=1,int pageSize=20)
        {
            var data = _recuimentService.GetAll(pageIndex:pageIndex, pageSize:pageSize,cateId:cateId,locationId:locationId);
            return View(data);
        }
        public ActionResult Detail(int id)
        {
            var item = _recuimentService.GetById(id);
            return View(item);
        }
	}
}