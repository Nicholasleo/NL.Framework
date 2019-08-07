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
        private IMenuBll _IMenuBll;

        public HomeController(IMenuBll menuBll)
        {
            _IMenuBll = menuBll;
        }

        public ActionResult Index()
        {
            Guid roleid = Common.Cache.Session.GetSession<LoginUserEnt>("NLFRAME_LOGIN_TOKEN").RoleId;
            List<NvaMenus> menuList = _IMenuBll.GetMenuList(roleid);
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