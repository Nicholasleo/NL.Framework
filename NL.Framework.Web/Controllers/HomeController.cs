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
            //UserModel user = _context.GetEntity<UserModel>(t=>t.UserName.Equals("NicholasLeo"));
            //user.UserAge = 20;
            //user.QQ = "461183790";
            //user.ModifyTime = DateTime.Now;
            //user.LastLoginTime = DateTime.Now;
            ////Console.WriteLine(_context.Insert(user)); 
            //_context.Update<UserModel>(user);

            //UserModel user = new UserModel
            //{
            //    UserName = "NicholasLeo",
            //    UserCode = "1001",
            //    UserPwd = "123456",
            //    UserAge = 20,
            //    QQ = "461183790",
            //    CreateTime = DateTime.Now,
            //    CreatePerson = "NicholasLeo"
            //};
            //_context.Insert(CreateMenu());
            //_context.Insert<FunctionModel>(CreateFunction());
            //_context.Insert<MenuFunctionModel>(CreateMenuFunction());
            //if (_context.IsExist<UserModel>(t => t.UserCode == "1001"))
            //{
            //    user = _context.GetEntity<UserModel>(t => t.UserCode.Equals("1001"));
            //    user.UserAge = 20;
            //    user.QQ = "461183790";
            //    user.ModifyTime = DateTime.Now;
            //    user.LastLoginTime = DateTime.Now;
            //    _context.Update(user);
            //}
            //else
            //{
            //    _context.Insert(user);
            //}
            //_ISystemInit.InitMenu();
            //_ISystemInit.InitFunction();
            //_ISystemInit.InitMenuFunction();
            _ISystemInit.InitRole();

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