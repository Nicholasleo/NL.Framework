//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-04 14:12:38
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

namespace NL.Framework.BLL
{
    public  abstract class CommonBll<T> : ISystemBaseBll<T> where T : BaseModel
    {
        private readonly IDbContext _IDbContext;
        private readonly ILogger _ILogger;
        public CommonBll(IDbContext db,ILogger logger)
        {
            _ILogger = logger;
            _IDbContext = db;
        }

        public virtual List<FunctionModel> GetMenuFunction(Guid menuFid,Guid roleFid)
        {
            MenuModel menu = _IDbContext.GetEntity<MenuModel>(t => t.Fid.Equals(menuFid));
            var r = from f in _IDbContext.Set<FunctionModel>()
                    join fm in _IDbContext.Set<RoleMenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in _IDbContext.Set<RoleMenuModel>()
                    on fm.RoleMenuId equals m.Fid
                    join rol in _IDbContext.Set<RoleModel>()
                    on m.RoleId equals rol.Fid
                    where m.MenuId.Equals(menu.Fid) && rol.Fid.Equals(roleFid)
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

        public virtual List<FunctionModel> GetMenuFunction(string menuName, Guid roleFid)
        {
            MenuModel menu = _IDbContext.GetEntity<MenuModel>(t => t.MenuName.Equals(menuName));
            var r = from f in _IDbContext.Set<FunctionModel>()
                    join fm in _IDbContext.Set<RoleMenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in _IDbContext.Set<RoleMenuModel>()
                    on fm.RoleMenuId equals m.Fid
                    join rol in _IDbContext.Set<RoleModel>()
                    on m.RoleId equals rol.Fid
                    where m.MenuId.Equals(menu.Fid) && rol.Fid.Equals(roleFid)
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
                m.FunctionEvent = item.FunctionEvent;
                m.Fid = item.Fid;
                m.FunctionName = item.FunctionName;
                flist.Add(m);
            }
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取菜单功能：{JsonConvert.SerializeObject(flist)}");
            }
            return flist;
        }
        public virtual List<FunctionModel> GetMenuFunction(Guid menuFid, string roleCode)
        {
            MenuModel menu = _IDbContext.GetEntity<MenuModel>(t => t.Fid.Equals(menuFid));
            var r = from f in _IDbContext.Set<FunctionModel>()
                    join fm in _IDbContext.Set<RoleMenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in _IDbContext.Set<RoleMenuModel>()
                    on fm.RoleMenuId equals m.Fid
                    join rol in _IDbContext.Set<RoleModel>()
                    on m.RoleId equals rol.Fid
                    where m.MenuId.Equals(menu.Fid) && rol.RoleCode.Equals(roleCode)
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
                m.FunctionEvent = item.FunctionEvent;
                m.Fid = item.Fid;
                m.FunctionName = item.FunctionName;
                flist.Add(m);
            }
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取菜单功能：{JsonConvert.SerializeObject(flist)}");
            }
            return flist;
        }
        public virtual List<FunctionModel> GetMenuFunction(string menuName, string roleCode)
        {
            MenuModel menu = _IDbContext.GetEntity<MenuModel>(t => t.MenuName.Equals(menuName));
            var r = from f in _IDbContext.Set<FunctionModel>()
                    join fm in _IDbContext.Set<RoleMenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in _IDbContext.Set<RoleMenuModel>()
                    on fm.RoleMenuId equals m.Fid
                    join rol in _IDbContext.Set<RoleModel>()
                    on m.RoleId equals rol.Fid
                    where m.MenuId.Equals(menu.Fid) && rol.RoleCode.Equals(roleCode)
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
                m.FunctionEvent = item.FunctionEvent;
                m.Fid = item.Fid;
                m.FunctionName = item.FunctionName;
                flist.Add(m);
            }
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取菜单功能：{JsonConvert.SerializeObject(flist)}");
            }
            return flist;
        }

        public virtual List<FunctionModel> GetMenuFunction()
        {
            MenuModel menu = _IDbContext.GetEntity<MenuModel>(t => t.MenuName == "菜单管理");
            var r = from f in _IDbContext.Set<FunctionModel>()
                    join fm in _IDbContext.Set<RoleMenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in _IDbContext.Set<RoleMenuModel>()
                    on fm.RoleMenuId equals m.Fid
                    join rol in _IDbContext.Set<RoleModel>()
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
                m.FunctionEvent = item.FunctionEvent;
                m.Fid = item.Fid;
                m.FunctionName = item.FunctionName;
                flist.Add(m);
            }
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取菜单功能：{JsonConvert.SerializeObject(flist)}");
            }
            return flist;
        }

        #region CRUD
        public virtual T GetModel(Guid fid)
        {
            throw new NotImplementedException();
        }

        public virtual AjaxResultEnt Update(T model)
        {
            throw new NotImplementedException();
        }

        public virtual AjaxResultEnt Create(T model)
        {
            throw new NotImplementedException();
        }

        public virtual AjaxResultEnt Delete(Guid fid)
        {
            throw new NotImplementedException();
        }

        public virtual AjaxResultEnt Delete(List<T> lists)
        {
            throw new NotImplementedException();
        }
        public virtual IQueryable GetQueryable()
        {
            throw new NotImplementedException();
        }

        public virtual List<T> GetLists(int page, int limit, out int total, object obj)
        {
            throw new NotImplementedException();
        }
        public virtual List<T> GetLists()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
