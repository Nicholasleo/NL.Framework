//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:11:54
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.BLL
{
    public class MenuBll : IMenuBll
    {
        private readonly IDbContext _context;
        public MenuBll(IDbContext db)
        {
            _context = db;
        }

        public int AddMenu(MenuModel model)
        {
            if (_context.IsExist<MenuModel>(model.Fid))
                return 0;
            model.CreateTime = DateTime.Now;
            return _context.Insert(model);
        }

        public int UpdateMenu(MenuModel model)
        {
            if (_context.IsExist<MenuModel>(model.Fid))
            {
                MenuModel menuModel = _context.GetEntity<MenuModel>(model.Fid);
                menuModel.MenuIcon = model.MenuIcon;
                menuModel.MenuIndex = model.MenuIndex;
                menuModel.MenuIsShow = model.MenuIsShow;
                menuModel.MenuName = model.MenuName;
                menuModel.MenuParentId = model.MenuParentId;
                menuModel.MenuUrl = model.MenuUrl;
                menuModel.ModifyPerson = "NicholasLeo";
                menuModel.ModifyTime = DateTime.Now;
                return _context.Update(menuModel);
            }
            return 0;
        }

        public int DeleteMenu(MenuModel model)
        {
            if (_context.IsExist<MenuModel>(model.Fid))
                return _context.Delete<MenuModel>(model.Fid);
            return 0;
        }

        public List<NvaMenus> GetMenuList()
        {
            IQueryable menus = _context.GetLists<MenuModel>();

            List<NvaMenus> menuList = new List<NvaMenus>();
            //获取根节点
            var roots = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(Guid.Empty));
            //通过根节点获取对应的子节点
            foreach (MenuModel root in roots)
            {
                if (root.MenuIsShow == 1)
                {
                    NvaMenus nva = new NvaMenus();
                    nva.MenuName = root.MenuName;
                    nva.MenuUrl = root.MenuUrl;
                    nva.MenuIcon = root.MenuIcon;
                    nva.MenuIndex = root.MenuIndex;
                    nva.Fid = root.Fid;
                    List<MenuModel> childMenus = new List<MenuModel>();
                    var childs = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(root.Fid));
                    foreach (MenuModel child in childs)
                    {
                        if (child.MenuIsShow == 1)
                        {
                            MenuModel menu = new MenuModel();
                            menu.Fid = child.Fid;
                            menu.MenuParentId = child.MenuParentId;
                            menu.MenuName = child.MenuName;
                            menu.MenuUrl = child.MenuUrl;
                            menu.MenuIndex = child.MenuIndex;
                            childMenus.Add(menu);
                        }
                    }
                    nva.ChildMenus = childMenus.OrderBy(t => t.MenuIndex).ToList();
                    menuList.Add(nva);
                }
            }
            return menuList.OrderBy(t=>t.MenuIndex).ToList();
        }

        public List<MenuModel> GetMenuLists(int page, int limit, out int total, string filtter = "")
        {
            Expression<Func<MenuModel, bool>> where = null;
            if (!string.IsNullOrEmpty(filtter))
                where = t => t.Fid.ToString().Equals(filtter) || t.MenuParentId.ToString().Equals(filtter);
            IQueryable data = _context.GetLists<MenuModel>(page, limit, out total, where);
            List<MenuModel> result = new List<MenuModel>();
            foreach (MenuModel item in data)
            {
                result.Add(item);
            }
            return result;
        }

        public List<FunctionModel> GetMenuFunction()
        {
            MenuModel menu = _context.GetEntity<MenuModel>(t => t.MenuName == "菜单管理");
            var r = from f in _context.Set<FunctionModel>()
                    join fm in _context.Set<RoleMenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in _context.Set<RoleMenuModel>()
                    on fm.RoleMenuId equals m.Fid
                    join rol in _context.Set<RoleModel>()
                    on m.RoleId equals rol.Fid
                    where m.MenuId.Equals(menu.Fid) && rol.RoleCode.Equals("SuperAdmin")
                    select new
                    {
                        FunctionName = f.FunctionName,
                        FunctionEvent = f.FunctionEvent
                    };
            List<FunctionModel> flist = new List<FunctionModel>();
            foreach (var item in r.ToList())
            {
                FunctionModel m = new FunctionModel();
                m.FunctionEvent = item.FunctionEvent;
                m.FunctionName = item.FunctionName;
                flist.Add(m);
            }
            return flist;
        }

        public List<FunctionModel> GetMenuFunction(Guid menuFid, Guid roleFid)
        {
            return CommonBll.GetMenuFunction(_context, menuFid, roleFid);
        }

        public List<FunctionModel> GetMenuFunction(Guid menuFid, string roleCode)
        {
            return CommonBll.GetMenuFunction(_context, menuFid, roleCode);
        }

        public List<FunctionModel> GetMenuFunction(string menuName, string roleCode)
        {
            return CommonBll.GetMenuFunction(_context, menuName, roleCode);
        }

        public List<FunctionModel> GetMenuFunction(string menuName, Guid roleFid)
        {
            return CommonBll.GetMenuFunction(_context, menuName, roleFid);
        }

        public IQueryable GetParentMenu()
        {
            return _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(Guid.Empty));
        }

        public MenuModel GetMenuModel(Guid fid)
        {
            return _context.GetEntity<MenuModel>(fid);
        }
    }
}
