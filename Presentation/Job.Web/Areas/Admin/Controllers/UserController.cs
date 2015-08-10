using Job.Core.Domain;
using Job.Services.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Job.Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        //
        // GET: /Admin/User/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            var data = _userService.GetAll();
            return View(data);
        }
        public ActionResult Create(int id = 0)
        {
            var user = new User();
            if (id > 0)
            {
                user = _userService.GetById(id);
                user.RePassword = user.Password;
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Create(User model, string OldPass)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    _userService.Insert(model);
                }
                else
                {
                    var user = _userService.GetUserByUserName(model.UserName);

                    user.Name = model.Name;
                    if (OldPass != null)
                    {
                        if (user.Password != OldPass)
                        {
                            ViewBag.mgs = "Mật khẩu cũ không đúng";
                            return View(model);
                        }

                        user.Password = model.Password;
                    }

                    _userService.Update(user);

                }
                return RedirectToAction("List");
            }
            return View(model);
        }
    }
}