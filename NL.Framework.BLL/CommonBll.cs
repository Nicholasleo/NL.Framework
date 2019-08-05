//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-04 14:12:38
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.BLL
{
    public  static partial class CommonBll
    {
        public static List<FunctionModel> GetMenuFunction(this IDbContext db, Guid menuFid,Guid roleFid)
        {
            MenuModel menu = db.GetEntity<MenuModel>(t => t.Fid.Equals(menuFid));
            var r = from f in db.Set<FunctionModel>()
                    join fm in db.Set<RoleMenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in db.Set<RoleMenuModel>()
                    on fm.RoleMenuId equals m.Fid
                    join rol in db.Set<RoleModel>()
                    on m.RoleId equals rol.Fid
                    where m.MenuId.Equals(menu.Fid) && rol.Fid.Equals(roleFid)
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

        public static List<FunctionModel> GetMenuFunction(this IDbContext db, string menuName, Guid roleFid)
        {
            MenuModel menu = db.GetEntity<MenuModel>(t => t.MenuName.Equals(menuName));
            var r = from f in db.Set<FunctionModel>()
                    join fm in db.Set<RoleMenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in db.Set<RoleMenuModel>()
                    on fm.RoleMenuId equals m.Fid
                    join rol in db.Set<RoleModel>()
                    on m.RoleId equals rol.Fid
                    where m.MenuId.Equals(menu.Fid) && rol.Fid.Equals(roleFid)
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
        public static List<FunctionModel> GetMenuFunction(this IDbContext db, Guid menuFid, string roleCode)
        {
            MenuModel menu = db.GetEntity<MenuModel>(t => t.Fid.Equals(menuFid));
            var r = from f in db.Set<FunctionModel>()
                    join fm in db.Set<RoleMenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in db.Set<RoleMenuModel>()
                    on fm.RoleMenuId equals m.Fid
                    join rol in db.Set<RoleModel>()
                    on m.RoleId equals rol.Fid
                    where m.MenuId.Equals(menu.Fid) && rol.RoleCode.Equals(roleCode)
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
        public static List<FunctionModel> GetMenuFunction(this IDbContext db, string menuName, string roleCode)
        {
            MenuModel menu = db.GetEntity<MenuModel>(t => t.MenuName.Equals(menuName));
            var r = from f in db.Set<FunctionModel>()
                    join fm in db.Set<RoleMenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in db.Set<RoleMenuModel>()
                    on fm.RoleMenuId equals m.Fid
                    join rol in db.Set<RoleModel>()
                    on m.RoleId equals rol.Fid
                    where m.MenuId.Equals(menu.Fid) && rol.RoleCode.Equals(roleCode)
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
    }
}
