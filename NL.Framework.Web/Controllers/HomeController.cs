using NL.Framework.IBLL;
using NL.Framework.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMenuBll _IMenuBll;

        public HomeController(IMenuBll menuBll)
        {
            _IMenuBll = menuBll;
        }

        public ActionResult Index()
        {
            LoginUserEnt ent = Common.Cache.Session.GetSession<LoginUserEnt>("NLFRAME_LOGIN_TOKEN");
            List<NvaMenus> menuList = _IMenuBll.GetMenuList(ent.RoleId);
            ViewBag.Title = "NLFrame";
            ViewBag.SystemName = "NLFrame";
            ViewBag.UserName = ent.UserName;
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