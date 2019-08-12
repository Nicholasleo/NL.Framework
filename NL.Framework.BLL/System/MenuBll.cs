//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:11:54
//    说明：
//    版权所有：个人
//***********************************************************
using Newtonsoft.Json;
using NL.Framework.Common;
using NL.Framework.Common.Log;
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NL.Framework.BLL
{
    public class MenuBll : CommonBll<MenuModel>,IMenuBll
    {
        #region Fields
        private readonly IDbContext _context;
        private readonly ILogger _ILogger;
        private static AjaxResultEnt result = new AjaxResultEnt
        {
            Code = 404,
            Message = "菜单操作错误！"
        };
        #endregion

        #region Ctor
        public MenuBll(IDbContext db
            , ILogger logger) : base(db, logger)
        {
            _ILogger = logger;
            _context = db;
        }
        #endregion

        #region Override
        public override AjaxResultEnt Create(MenuModel model)
        {
            if (_context.IsExist<MenuModel>(t => t.MenuName.Equals(model.MenuName)))
            {
                result.Code = 503;
                result.Message = $"{model.MenuName}已存在!";
                return result;
            }
            model.CreateTime = DateTime.Now;
            model.CreatePerson = OperatorProvider.Provider.GetCurrent().UserName;
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"添加菜单：{JsonConvert.SerializeObject(model)}");
            }
            int i = _context.Insert(model);
            if (i > 0)
            {
                result.Code = 200;
                result.Message = "菜单添加成功!";
            }
            else
            {
                result.Code = 404;
                result.Message = "菜单添加失败!";
            }
            return result;
        }

        public override AjaxResultEnt Update(MenuModel model)
        {
            result.Code = 503;
            result.Message = "修改的菜单不存在!";
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
                if (OperatorProvider.Provider.IsDebug)
                {
                    _ILogger.Debug($"修改菜单：{JsonConvert.SerializeObject(model)}");
                }
                int i = _context.Update(menuModel);
                if (i > 0)
                {
                    result.Code = 200;
                    result.Message = "菜单修改成功!";
                }
                else
                {
                    result.Code = 404;
                    result.Message = "菜单修改失败!";
                }
            }
            return result;
        }

        public override AjaxResultEnt Delete(List<MenuModel> meuns)
        {
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
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"删除菜单：{JsonConvert.SerializeObject(meuns)}");
            }
            int state = _context.UsingTransaction(action) > 0 ? 200 : 404;
            result.Code = state;
            if (state == 200)
                result.Message = $"删除【{menuname.TrimEnd(',')}】成功！";
            return result;
        }

        public override List<MenuModel> GetLists(int page, int limit, out int total, object obj)
        {
            string filtter = obj.ToString();
            Expression<Func<MenuModel, bool>> where = null;
            if (!string.IsNullOrEmpty(filtter))
                where = t => t.Fid.ToString().Equals(filtter) || t.MenuParentId.ToString().Equals(filtter);
            IQueryable data = _context.GetLists<MenuModel>(page, limit, out total, where);
            List<MenuModel> result = new List<MenuModel>();
            foreach (MenuModel item in data)
            {
                result.Add(item);
            }
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取菜单列表：{JsonConvert.SerializeObject(result)}");
            }
            return result;
        }

        public override MenuModel GetModel(Guid fid)
        {
            MenuModel model = _context.GetEntity<MenuModel>(fid);
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取菜单：{JsonConvert.SerializeObject(model)}");
            }
            return model;
        }
        /// <summary>
        /// GetParentMenu
        /// </summary>
        /// <returns></returns>
        public override IQueryable GetQueryable()
        {
            IQueryable result = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(Guid.Empty));
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取一级菜单：{JsonConvert.SerializeObject(result)}");
            }
            return result;
        }
        public override List<FunctionModel> GetMenuFunction()
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
                        Fid = f.Fid,
                        FunctionName = f.FunctionName,
                        FunctionEvent = f.FunctionEvent
                    };
            List<FunctionModel> flist = new List<FunctionModel>();
            foreach (var item in r.ToList())
            {
                FunctionModel m = new FunctionModel();
                m.Fid = item.Fid;
                m.FunctionEvent = item.FunctionEvent;
                m.FunctionName = item.FunctionName;
                flist.Add(m);
            }
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取菜单功能：{JsonConvert.SerializeObject(flist)}");
            }
            return flist;
        }
        #endregion

        #region Private
        public List<NvaMenus> GetMenuList(Guid roleid)
        {
            IQueryable menus = _context.GetLists<MenuModel>();
            RoleModel role = _context.GetEntity<RoleModel>(roleid);
            if (role.RoleCode.ToLower() == "superadmin")
                return GetMenuList();

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

            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取菜单列表：{JsonConvert.SerializeObject(menuList.OrderBy(t => t.MenuIndex).ToList())}");
            }
            return menuList.OrderBy(t => t.MenuIndex).ToList();
        }
        #endregion

        #region Methods
        /// <summary>
        /// 超级管理员
        /// </summary>
        /// <returns></returns>
        private List<NvaMenus> GetMenuList()
        {
            List<NvaMenus> menuList = new List<NvaMenus>();
            //获取根节点 -- 通过角色获取菜单
            var roots = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(Guid.Empty));
            //var roots = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(Guid.Empty));
            //通过根节点获取对应的子节点
            foreach (MenuModel root in roots.AsQueryable())
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
                    foreach (MenuModel child in childs.AsQueryable())
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

            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取菜单列表：{JsonConvert.SerializeObject(menuList.OrderBy(t => t.MenuIndex).ToList())}");
            }
            return menuList.OrderBy(t => t.MenuIndex).ToList();
        }
        #endregion

    }
}
