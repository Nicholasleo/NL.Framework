//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:59:36
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NL.Framework.BLL
{
    public class RoleBll : IRoleBll
    {
        private readonly IDbContext _context;

        public RoleBll(IDbContext db)
        {
            _context = db;
        }
        public int AddRole(RoleModel model)
        {
            throw new NotImplementedException();
        }

        public int DeleteRole(RoleModel model)
        {
            return _context.Delete<RoleModel>(model.Fid);
        }

        public List<FunctionModel> GetMenuFunction()
        {
            var menu = _context.GetEntity<MenuModel>(t => t.MenuName == "角色管理");
            var r = from f in _context.Set<FunctionModel>()
                    join fm in _context.Set<MenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in _context.Set<MenuModel>()
                    on fm.MenuId equals m.Fid
                    where m.Fid == menu.Fid
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

        public IQueryable GetRoleAll()
        {
            return _context.GetLists<RoleModel>();
        }

        public RoleModel GetRoleModel(string fid)
        {
            return _context.GetEntity<RoleModel>(t => t.Fid.ToString().Equals(fid));
        }

        public List<RoleModel> GetRolesLists(int page, int limit,out int total, string role = "")
        {
            Expression<Func<RoleModel, bool>> where = null;
            if (!string.IsNullOrEmpty(role))
                where = t => t.Fid.ToString() == role;
            IQueryable data = _context.GetLists<RoleModel>(page, limit, out total, where);
            List<RoleModel> result = new List<RoleModel>();
            foreach (RoleModel item in data)
            {
                result.Add(item);
            }
            return result;
        }

        public int UpdateRole(RoleModel model)
        {
            RoleModel m = _context.GetEntity<RoleModel>(model.Fid);
            m.Description = model.Description;
            m.ModifyTime = DateTime.Now;
            m.ModifyPerson = "NicholasLeo";
            return _context.Update(m);
        }
    }
}
