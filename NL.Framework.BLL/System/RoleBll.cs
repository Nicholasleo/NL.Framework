//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:59:36
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
    public class RoleBll : CommonBll<RoleModel>,IRoleBll
    {
        #region Fields
        private readonly IDbContext _context;
        private readonly ILogger _ILogger;
        private static AjaxResultEnt result = new AjaxResultEnt
        {
            Code = 404,
            Message = "角色操作错误！"
        };
        #endregion

        #region Ctor
        public RoleBll(IDbContext db, ILogger logger) : base(db, logger)
        {
            _ILogger = logger;
            _context = db;
        }
        #endregion

        #region Override
        public override List<RoleModel> GetLists()
        {
            var result = from r in _context.Set<RoleModel>()
                         select r;
            return result.ToList();
        }
        public override List<RoleModel> GetLists(int page, int limit, out int total, object obj)
        {
            string role = obj.ToString();
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
        public override AjaxResultEnt Update(RoleModel model)
        {
            RoleModel m = _context.GetEntity<RoleModel>(model.Fid);
            m.Description = model.Description;
            m.ModifyTime = DateTime.Now;
            m.ModifyPerson = OperatorProvider.Provider.GetCurrent().UserName;
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"修改角色：{JsonConvert.SerializeObject(m)}");
            }
            int i = _context.Update(m);
            if (i > 0)
            {
                result.Code = 200;
                result.Message = "修改角色成功!";
            }
            else
            {
                result.Code = 503;
                result.Message = "修改角色失败!";
            }
            return result;
        }
        public override AjaxResultEnt Create(RoleModel model)
        {
            model.CreateTime = DateTime.Now;
            model.CreatePerson = OperatorProvider.Provider.GetCurrent().UserName;
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"新增角色：{JsonConvert.SerializeObject(model)}");
            }
            int i = _context.Insert<RoleModel>(model);
            if (i > 0)
            {
                result.Code = 200;
                result.Message = "新增角色成功!";
            }
            else
            {
                result.Code = 503;
                result.Message = "新增角色失败!";
            }
            return result;
        }
        public override AjaxResultEnt Delete(List<RoleModel> lists)
        {
            result.Code = 503;
            result.Message = "删除角色失败！";
            string rolename = "";
            Action<IDbContext> action = new Action<IDbContext>((IDbContext db) => {
                foreach (RoleModel model in lists)
                {
                    if (model.RoleCode.Equals("SuperAdmin"))
                        continue;
                    if (_context.IsExist<RoleModel>(model.Fid))
                    {
                        int i = db.Delete<RoleModel>(model.Fid);
                        if (i > 0)
                            rolename += model.RoleName + ",";
                    }
                }
            });
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"删除角色：{JsonConvert.SerializeObject(lists)}");
            }
            int state = _context.UsingTransaction(action) > 0 ? 200 : 404;
            result.Code = state;
            if (state == 200)
                result.Message = $"删除【{rolename.TrimEnd(',')}】成功！";
            return result;
        }
        public override IQueryable GetQueryable()
        {
            return _context.GetLists<RoleModel>();
        }
        public override RoleModel GetModel(Guid fid)
        {
            return _context.GetEntity<RoleModel>(fid);
        }
        public override List<FunctionModel> GetMenuFunction()
        {
            MenuModel menu = _context.GetEntity<MenuModel>(t => t.MenuName == "角色管理");
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
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取菜单功能：{JsonConvert.SerializeObject(flist)}");
            }
            return flist;
        }
        #endregion

        #region Private
        #endregion

        #region Methods
        #endregion
    }
}
