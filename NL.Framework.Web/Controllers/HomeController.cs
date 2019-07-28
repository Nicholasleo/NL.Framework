using NL.Framework.DAL;
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
        private IDbContext _context;

        public HomeController(IDbContext db)
        {
            _context = db;
        }
        public ActionResult Index()
        {
            UserModel user = _context.GetEntity<UserModel>(t=>t.UserName.Equals("NicholasLeo"));
            user.UserAge = 20;
            user.QQ = "461183790";
            user.ModifyTime = DateTime.Now;
            user.LastLoginTime = DateTime.Now;
            //Console.WriteLine(_context.Insert(user)); 
            _context.Update<UserModel>(user);

            ViewBag.Title = "测试";
            ViewBag.SystemName = "NLFrame";
            ViewBag.UserName = "NicholasLeo";
            //系统菜单模型
            List<NvaMenus> menuList = new List<NvaMenus>();
            Guid fid = Guid.NewGuid();
            NvaMenus nva = new NvaMenus();
            nva.MenuName = "系统管理菜单";
            nva.MenuUrl = "";
            nva.MenuIcon = "layui-icon-set";
            nva.Fid = fid;
            List<MenuModel> childMenus = new List<MenuModel>();
            MenuModel menu = new MenuModel();
            menu.Fid = Guid.NewGuid();
            menu.MenuParentId = fid;
            menu.MenuName = "用户管理";
            menu.MenuUrl = "/Login/Index";
            childMenus.Add(menu);
            menu = new MenuModel();
            menu.Fid = Guid.NewGuid();
            menu.MenuParentId = fid;
            menu.MenuName = "角色管理";
            menu.MenuUrl = "/System/Role";
            childMenus.Add(menu);
            menu = new MenuModel();
            menu.Fid = Guid.NewGuid();
            menu.MenuParentId = fid;
            menu.MenuName = "菜单管理";
            menu.MenuUrl = "/System/Menu";
            childMenus.Add(menu);
            menu = new MenuModel();
            menu.Fid = Guid.NewGuid();
            menu.MenuParentId = fid;
            menu.MenuName = "权限管理";
            menu.MenuUrl = "/System/Right";
            childMenus.Add(menu);
            nva.ChildMenus = childMenus;
            menuList.Add(nva);
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