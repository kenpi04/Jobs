using Job.Core.Domain;
using Job.Services.Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job.Web.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Admin/Common/
        private readonly ICommonService _commonService;
        public CommonController(ICommonService commonService)
        {
            this._commonService = commonService;
        }
        public ActionResult ListShop()
        {
            var shops = _commonService.GetAllShop();
            return View(shops);
        }
        public ActionResult CreateUpdateShop(int id=0)
        {
            var model=new Shop();
            if (id != 0)
            {
                model = _commonService.GetShopById(id);
                ViewBag.StateId = model.District.StateProvice.Id;
            }
            ViewBag.StateList = _commonService.GetAllStateProvince();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateUpdateShop(Shop model)
        {
            if(ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _commonService.InsertShop(model);
                }
                else
                {
                    var shop = _commonService.GetShopById(model.Id);
                    shop.Name = model.Name;
                    shop.ShopAddress = model.ShopAddress;
                    shop.LocationId = model.LocationId;
                    shop.PhoneNumber = model.PhoneNumber;
                    _commonService.UpdateShop(shop);
                }
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Cập nhật không thành công";
            return View(model);

            
        }
	}
}