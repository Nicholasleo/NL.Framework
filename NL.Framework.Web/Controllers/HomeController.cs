using NL.Framework.Common;
using NL.Framework.Common.Config;
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
            LoginUserEnt ent = OperatorProvider.Provider.GetCurrent();
            List<NvaMenus> menuList = _IMenuBll.GetMenuList(ent.RoleId);
            ViewBag.Title = Configs.GetValue(SystemParameters.NLFRAME_SYSTEM_NAME);
            ViewBag.SystemName = Configs.GetValue(SystemParameters.NLFRAME_SYSTEM_NAME);
            ViewBag.Version = Configs.GetValue(SystemParameters.NLFRAME_SYSTEM_VERSION);
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