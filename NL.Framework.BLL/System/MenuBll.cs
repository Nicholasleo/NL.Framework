//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:11:54
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Common;
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
                menuModel.ModifyPerson = OperatorProvider.Provider.GetCurrent().UserName;
                menuModel.ModifyTime = DateTime.Now;
                return _context.Update(menuModel);
            }
            return 0;
        }

        public AjaxResultEnt DeleteMenu(List<MenuModel> meuns)
        {
            AjaxResultEnt result = new AjaxResultEnt();
            result.Code = 503;
            result.Message = "删除菜单失败！";
            string menuname = "";
            Action<IDbContext> action = new Action<IDbContext>((IDbContext db) => {
                foreach (MenuModel model in meuns)
                {
                    if (_context.IsExist<MenuModel>(model.Fid))
                    {
                        int i = db.Delete<MenuModel>(model.Fid);
                        if (i > 0)
                            menuname += model.MenuName + ",";
                    }
                }
            });

            int state = _context.UsingTransaction(action) > 0 ? 200 : 404;
            result.Code = state;
            if (state == 200)
                result.Message = $"删除【{menuname.TrimEnd(',')}】成功！";
            return result;
        }

        public List<NvaMenus> GetMenuList(Guid roleid)
        {
            IQueryable menus = _context.GetLists<MenuModel>();

            List<NvaMenus> menuList = new List<NvaMenus>();
            //获取根节点 -- 通过角色获取菜单
            var roots = from menu in _context.Set<MenuModel>()
                      join rm in _context.Set<RoleMenuModel>()
                      on menu.Fid equals rm.MenuId
                      where rm.RoleId.Equals(roleid) && menu.MenuParentId.Equals(Guid.Empty)
                      select menu;
            //var roots = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(Guid.Empty));
            //通过根节点获取对应的子节点
            foreach (MenuModel root in roots.AsEnumerable())
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
                    //var childs = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(root.Fid));
                    var childs = from menu in _context.Set<MenuModel>()
                                 join rm in _context.Set<RoleMenuModel>()
                                 on menu.Fid equals rm.MenuId
                                 where rm.RoleId.Equals(roleid) && menu.MenuParentId.Equals(root.Fid)
                                 select menu;
                    foreach (MenuModel child in childs.AsEnumerable())
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
                    where m.MenuId.Equals(menu.Fid) && rol.RoleCode.Equals(OperatorProvider.Provider.GetCurrent().RoleCode)
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
