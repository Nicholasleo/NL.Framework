using NL.Framework.IBLL;
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginBll _ILoginBll = null;
        private readonly ISystemInit _ISystemInit;
        public LoginController(ISystemInit systemInit, ILoginBll loginBll)
        {
            _ISystemInit = systemInit;
            _ILoginBll = loginBll;
        }
        // GET: Login
        public ActionResult Index()
        {
            //_ISystemInit.InitMenu();
            //_ISystemInit.InitFunction();
            //_ISystemInit.InitMenuFunction();
            _ISystemInit.InitRole();
            _ISystemInit.InitUser();
            _ISystemInit.InitUserRole();
            return View();
        }

        [HttpPost]
        public JsonResult CheckLogin(LoginEnt model)
        {
            LoginStatusEnt result = _ILoginBll.CheckUserLogin(model);
            return Json(result);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Common.Cache.Session.SetCookie("NLFRAME_LOGIN_TOKEN",null);
            return new RedirectResult("/Login/Index");
        }
    }
}