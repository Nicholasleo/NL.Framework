using NL.Framework.DAL;
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public class HomeController : Controller
    {
        private ISystemInit _ISystemInit;
        private IMenuBll _IMenuBll;

        public HomeController(ISystemInit systemInit, IMenuBll menuBll)
        {
            _ISystemInit = systemInit;
            _IMenuBll = menuBll;
        }

        public ActionResult Index()
        {
            //_ISystemInit.InitMenu();
            //_ISystemInit.InitFunction();
            //_ISystemInit.InitMenuFunction();
            _ISystemInit.InitRole();
            _ISystemInit.InitUser();

            List<NvaMenus> menuList = _IMenuBll.GetMenuList();
            ViewBag.Title = "测试";
            ViewBag.SystemName = "NLFrame";
            ViewBag.UserName = "NicholasLeo";
            return View(menuList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}