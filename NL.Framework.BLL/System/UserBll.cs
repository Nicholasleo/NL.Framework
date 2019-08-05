using NL.Framework.Common;
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model.NLFrameEnt;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-01 15:14:14 
//    说明：
//    版权所有：个人
//***********************************************************
namespace NL.Framework.BLL
{
    public class UserBll : IUserBll
    {
        private readonly IDbContext _context;
        public UserBll(IDbContext db)
        {
            _context = db;
        }
        public int AddUser(UserModel model)
        {
            return _context.Insert<UserModel>(model);
        }

        public int DeleteUser(UserModel model)
        {
            if (model.UserCode.ToLower().Equals("admin") || model.UserCode.ToLower().Equals("nicholasleo"))
                return 0;
            return _context.Delete<UserModel>(model.Fid);
        }

        public List<FunctionModel> GetMenuFunction()
        {
            MenuModel menu = _context.GetEntity<MenuModel>(t => t.MenuName == "用户管理");
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

        public IQueryable GetUserAll()
        {
            return _context.GetLists<UserModel>();
        }



        public List<UserModel> GetUserLists(int page, int limit, out int total, UserPageEnt pageEnt)
        {
            Expression<Func<UserModel, bool>> where = null;
            if (!string.IsNullOrEmpty(pageEnt.UserCode))
            {
                if (where == null)
                    where = t => t.UserCode.Equals(pageEnt.UserCode);
                else
                    where = ExpressionHelp.ExpressionAnd(where, where);
            }
            if (!string.IsNullOrEmpty(pageEnt.UserName))
            {
                if (where == null)
                    where = t => t.UserName.Equals(pageEnt.UserName);
                else
                    where = ExpressionHelp.ExpressionAnd(where, where);
            }
            if (pageEnt.Gender != 3)
            {
                if (where == null)
                    where = t => t.Gender.Equals(pageEnt.Gender);
                else
                    where = ExpressionHelp.ExpressionAnd(where, where);
            }
            if (!string.IsNullOrEmpty(pageEnt.Email))
            {
                if (where == null)
                    where = t => t.Email.Equals(pageEnt.Email);
                else
                    where = ExpressionHelp.ExpressionAnd(where, where);
            }
            //where = t => t.Fid.ToString() == role;
            IQueryable data = _context.GetLists(page, limit, out total, where);
            List<UserModel> result = new List<UserModel>();
            foreach (UserModel item in data)
            {
                result.Add(item);
            }
            return result;
        }

        public UserModel GetUserModel(Guid fid)
        {
            return _context.GetEntity<UserModel>(fid);
        }

        public int UpdateUser(UserModel model)
        {
            UserModel userModel = _context.GetEntity<UserModel>(model.Fid);
            if (userModel != null)
            {
                userModel.Description = model.Description;
                userModel.Address = model.Address;
                userModel.Email = model.Email;
                userModel.Gender = model.Gender;
                userModel.IdCard = model.IdCard;
                userModel.IsAdmin = model.IsAdmin;
                userModel.IsDelete = model.IsDelete;
                userModel.CreatePerson = model.CreatePerson;
                userModel.CreateTime = model.CreateTime;
                userModel.MobilePhone = model.MobilePhone;
                userModel.ModifyPerson = "NIcholasLeo";
                userModel.ModifyTime = DateTime.Now;
                userModel.QQ = model.QQ;
                userModel.State = model.State;
                userModel.UserAge = model.UserAge;
                userModel.UserName = model.UserName;
                userModel.UserPwd = model.UserPwd;
                userModel.WeChat = model.WeChat;
                return _context.Update(userModel);
            }
            return 0;
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
    }
}
