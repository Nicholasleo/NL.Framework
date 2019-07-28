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

        private List<MenuModel> CreateMenu()
        {
            List<MenuModel> list = new List<MenuModel>();
            Guid fid = Guid.NewGuid();
            MenuModel menu = new MenuModel
            {
                Fid = fid,
                MenuName = "系统管理菜单",
                MenuIcon = "layui-icon-set",
                MenuIndex = 1,
                MenuIsShow = 1,
                MenuUrl = "",
                MenuParentId = Guid.Empty
            };
            list.Add(menu);
            menu = new MenuModel
            {
                MenuName = "用户管理",
                MenuIcon = "layui-icon-user",
                MenuIndex = 1,
                MenuIsShow = 1,
                MenuUrl = "/System/UserIndex",
                MenuParentId = fid
            };
            list.Add(menu);
            menu = new MenuModel
            {
                MenuName = "角色管理",
                MenuIcon = "layui-icon-role",
                MenuIndex = 2,
                MenuIsShow = 1,
                MenuUrl = "/System/RoleIndex",
                MenuParentId = fid
            };
            list.Add(menu);
            menu = new MenuModel
            {
                MenuName = "菜单管理",
                MenuIcon = "layui-icon-menu",
                MenuIndex = 3,
                MenuIsShow = 1,
                MenuUrl = "/System/MenuIndex",
                MenuParentId = fid
            };
            list.Add(menu);
            menu = new MenuModel
            {
                MenuName = "权限管理",
                MenuIcon = "layui-icon-right",
                MenuIndex = 4,
                MenuIsShow = 1,
                MenuUrl = "/System/RightIndex",
                MenuParentId = fid
            };
            list.Add(menu);
            return list;
        }

        private List<FunctionModel> CreateFunction()
        {
            List<FunctionModel> list = new List<FunctionModel>
            {
               new FunctionModel{
                   FunctionName = "新增",
                    FunctionEvent = "add"
               },
               new FunctionModel{
                   FunctionName = "编辑",
                    FunctionEvent = "edit"
               },
               new FunctionModel{
                   FunctionName = "删除",
                    FunctionEvent = "delete"
               },
               new FunctionModel{
                   FunctionName = "导入",
                    FunctionEvent = "import"
               },
               new FunctionModel{
                   FunctionName = "导出",
                    FunctionEvent = "export"
               }
            };
            return list;
        }

        private List<MenuFunctionModel> CreateMenuFunction()
        {
            List<MenuFunctionModel> list = new List<MenuFunctionModel>();
            MenuModel menu = _context.GetEntity<MenuModel>(t => t.MenuName == "角色管理");
            IQueryable queryable = _context.GetLists<FunctionModel>();
            foreach (FunctionModel item in queryable)
            {
                MenuFunctionModel f = new MenuFunctionModel();
                f.MenuId = menu.Fid;
                f.FunctionId = item.Fid;
                f.CreatePerson = "NicholasLeo";
                f.CreateTime = DateTime.Now;
                list.Add(f);
            }
            return list;
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

            UserModel user = new UserModel
            {
                UserName = "NicholasLeo",
                UserCode = "1001",
                UserPwd = "123456",
                UserAge = 20,
                QQ = "461183790",
                CreateTime = DateTime.Now,
                CreatePerson = "NicholasLeo"
            };
            //_context.Insert(CreateMenu());
            //_context.Insert<FunctionModel>(CreateFunction());
            //_context.Insert<MenuFunctionModel>(CreateMenuFunction());
            if (_context.IsExist<UserModel>(t => t.UserCode == "1001"))
            {
                user = _context.GetEntity<UserModel>(t => t.UserCode.Equals("1001"));
                user.UserAge = 20;
                user.QQ = "461183790";
                user.ModifyTime = DateTime.Now;
                user.LastLoginTime = DateTime.Now;
                _context.Update(user);
            }
            else
            {
                _context.Insert(user);
            }

            RoleModel role = new RoleModel();
            role.RoleName = "系统超级管理员";
            role.CreatePerson = "NicholasLeo";
            role.CreateTime = DateTime.Now;
            role.RoleCode = "SuperAdmin";
            role.Description = "系统的超级权限拥有系统的所有权限";
            if(!_context.IsExist<RoleModel>(t=>t.RoleCode=="SuperAdmin"))
                _context.Insert<RoleModel>(role);
            new RoleModel();
            role.RoleName = "超级管理员";
            role.CreatePerson = "NicholasLeo";
            role.CreateTime = DateTime.Now;
            role.RoleCode = "Admin";
            role.Description = "系统的管理员，权限小于超级管理员";
            if (!_context.IsExist<RoleModel>(t => t.RoleCode == "Admin"))
                _context.Insert<RoleModel>(role);


            IQueryable menus = _context.GetLists<MenuModel>();
           
            List<NvaMenus> menuList = new List<NvaMenus>();
            NvaMenus nva = new NvaMenus();
            List<MenuModel> childMenus = new List<MenuModel>();
            foreach (MenuModel item in menus.AsQueryable())
            {
                if (item.MenuParentId.Equals(Guid.Empty))
                {
                    nva.MenuName = item.MenuName;
                    nva.MenuUrl = item.MenuUrl;
                    nva.MenuIcon = item.MenuIcon;
                    nva.Fid = item.Fid;
                }
                else
                {
                    MenuModel menu = new MenuModel();
                    menu.Fid = item.Fid;
                    menu.MenuParentId = item.MenuParentId;
                    menu.MenuName = item.MenuName;
                    menu.MenuUrl = item.MenuUrl;
                    childMenus.Add(menu);
                }
            }
            nva.ChildMenus = childMenus;
            menuList.Add(nva);


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