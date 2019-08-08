using NL.Framework.Common;
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
            //初始化菜单
            //_ISystemInit.InitMenu();
            //初始化功能
            //_ISystemInit.InitFunction();
            //初始化菜单功能关系
            //_ISystemInit.InitMenuFunction();
            //初始化角色
            _ISystemInit.InitRole();
            //初始化用户
            _ISystemInit.InitUser();
            //初始化用户角色
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
            Common.Cache.Session.SetSession("NLFRAME_LOGIN_TOKEN", null);
            DataPools.Instance.SetLoginInfo(null);
            return new RedirectResult("/Login/Index");
        }
    }
}