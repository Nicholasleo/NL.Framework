using NL.Framework.Common;
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model;
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
        public AjaxResultEnt AddUser(UserEditEnt model)
        {
            AjaxResultEnt result = new AjaxResultEnt();
            result.Code = 100;
            Action<IDbContext> action = new Action<IDbContext>((IDbContext db) => {
                if (db.IsExist<UserModel>(t => t.UserCode.ToLower().Equals(model.UserCode))) {
                    result.Code = 503;
                    result.Message = $"{model.UserCode}已存在！";
                    return;
                }
                if (db.IsExist<UserModel>(t => t.IdCard.ToLower().Equals(model.IdCard)))
                {
                    result.Code = 503;
                    result.Message = $"{model.IdCard}已存在！";
                    return;
                }
                UserModel userModel = new UserModel
                {
                    UserAge = model.UserAge,
                    UserName = model.UserName,
                    UserCode = model.UserCode,
                    UserPwd = model.UserPwd,
                    Address = model.Address,
                    CreatePerson = model.CreatePerson,
                    CreateTime = DateTime.Now,
                    Description = model.Description,
                    Email = model.Email,
                    Gender = model.Gender,
                    IdCard = model.IdCard,
                    IsAdmin = model.IsAdmin,
                    IsDelete = model.IsDelete,
                    QQ = model.QQ,
                    WeChat = model.WeChat,
                    MobilePhone = model.MobilePhone,
                    State = model.State
                };
                db.Insert<UserModel>(userModel);
                Guid userId = db.GetEntity<UserModel>(t => t.UserCode.Equals(model.UserCode)).Fid;
                UserRoleModel userRole = new UserRoleModel
                {
                    UserId = userId,
                    RoleId = model.RoleId,
                    CreatePerson = model.CreatePerson,
                    CreateTime = DateTime.Now
                };
                db.Insert<UserRoleModel>(userRole);
            });
            int flg = _context.UsingTransaction(action);
            if(result.Code == 100 && flg > 0) { 
                result.Code = 200;
                result.Message = "用户添加成功！";
            }
            return result;
        }

        public int DeleteUser(Guid fid)
        {
            UserModel model = _context.GetEntity<UserModel>(fid);
            if (model.UserCode.ToLower().Equals("admin") || model.UserCode.ToLower().Equals("nicholasleo"))
                return 0;
            return _context.Delete<UserModel>(fid);
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

        public UserEditEnt GetUserEidtModel(Guid fid)
        {
            Guid roleid = Guid.Empty;
            UserModel model = _context.GetEntity<UserModel>(fid);
            UserRoleModel uModel = _context.GetEntity<UserRoleModel>(t => t.UserId.Equals(fid));
            if(uModel != null)
                roleid = uModel.RoleId;
            UserEditEnt ent = new UserEditEnt
            {
                Fid = model.Fid,
                RoleId = roleid,
                UserCode = model.UserCode,
                UserName = model.UserName,
                UserPwd = model.UserPwd,
                IdCard = model.IdCard,
                Gender = model.Gender,
                UserAge = model.UserAge,
                Email = model.Email,
                WeChat = model.WeChat,
                QQ = model.QQ,
                MobilePhone = model.MobilePhone,
                Address = model.Address,
                IsDelete = model.IsDelete,
                IsAdmin = model.IsAdmin,
                State = model.State,
                LastLoginTime = model.LastLoginTime,
                FirstLoginTime = model.FirstLoginTime,
                Description = model.Description,
                CreateTime = model.CreateTime,
                CreatePerson = model.CreatePerson,
                ModifyTime = model.ModifyTime,
                ModifyPerson = model.ModifyPerson
            };
            return ent;
        }

        public UserRoleModel GetUserRoleModel(Guid fid)
        {
            return _context.GetEntity<UserRoleModel>(t=>t.UserId.Equals(fid));
        }

        public int UpdateUser(UserEditEnt model)
        {
            Action<IDbContext> action = new Action<IDbContext>((IDbContext db) => {
                UserModel userModel = db.GetEntity<UserModel>(model.Fid);
                //更新用户表
                if (userModel != null)
                {
                    userModel.Description = model.Description;
                    userModel.Address = model.Address;
                    userModel.Email = model.Email;
                    userModel.Gender = model.Gender;
                    userModel.IdCard = model.IdCard;
                    userModel.IsAdmin = model.IsAdmin;
                    userModel.IsDelete = model.IsDelete;
                    userModel.MobilePhone = model.MobilePhone;
                    userModel.ModifyPerson = OperatorProvider.Provider.GetCurrent().UserName;
                    userModel.ModifyTime = DateTime.Now;
                    userModel.QQ = model.QQ;
                    userModel.State = model.State;
                    userModel.UserAge = model.UserAge;
                    userModel.UserName = model.UserName;
                    //userModel.UserPwd = model.UserPwd;
                    userModel.WeChat = model.WeChat;
                    db.Update(userModel);
                }
                //更新角色绑定
                if (db.IsExist<UserRoleModel>(t => t.UserId.Equals(model.Fid)))
                {
                    UserRoleModel userRole = db.GetEntity<UserRoleModel>(t => t.UserId.Equals(model.Fid));
                    userRole.RoleId = model.RoleId;
                    userRole.ModifyPerson = OperatorProvider.Provider.GetCurrent().UserName;
                    userRole.ModifyTime = DateTime.Now;
                    db.Update(userRole);
                }
                else
                {
                    UserRoleModel userRole = new UserRoleModel();
                    userRole.UserId = model.Fid;
                    userRole.RoleId = model.RoleId;
                    userRole.CreatePerson = OperatorProvider.Provider.GetCurrent().UserName;
                    userRole.CreateTime = DateTime.Now;
                    db.Insert(userRole);
                }
            });
            return _context.UsingTransaction(action);
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

        public AjaxResultEnt DeleteUser(List<UserModel> users)
        {
            AjaxResultEnt result = new AjaxResultEnt();
            result.Code = 503;
            result.Message = "删除用户失败！";
            string username = "";
            Action<IDbContext> action = new Action<IDbContext>((IDbContext db) => {
                foreach (UserModel model in users)
                {
                    if (model.UserCode.ToLower().Equals("admin") || model.UserCode.ToLower().Equals("nicholasleo"))
                        continue;
                    int i = db.Delete<UserModel>(model.Fid);
                    if(i > 0)
                        username += model.UserName + ",";
                }
            });

            int state = _context.UsingTransaction(action) > 0 ? 200 : 404;
            result.Code = state;
            if (state == 200)
                result.Message = $"删除【{username.TrimEnd(',')}】成功！";

            return result;
        }
        public AjaxResultEnt UpdateUserRole(UserRoleEnt ent)
        {
            AjaxResultEnt result = new AjaxResultEnt();
            Func<IDbContext, AjaxResultEnt> func = new Func<IDbContext, AjaxResultEnt>((IDbContext db) =>
            {
                if (_context.IsExist<UserRoleModel>(t => t.UserId.Equals(ent.UserId)))
                {
                    UserRoleModel userRole = db.GetEntity<UserRoleModel>(t => t.UserId.Equals(ent.UserId));
                    userRole.RoleId = ent.RoleId;
                    userRole.ModifyTime = DateTime.Now;
                    userRole.ModifyPerson = OperatorProvider.Provider.GetCurrent().UserName;
                    result.Code = _context.Update(userRole) > 0 ? 200 : 503;
                    result.Message = "角色绑定成功！";
                }
                else
                {
                    UserRoleModel userRole = new UserRoleModel
                    {
                        UserId = ent.UserId,
                        RoleId = ent.RoleId,
                        CreatePerson = OperatorProvider.Provider.GetCurrent().UserName,
                        CreateTime = DateTime.Now
                    };
                    result.Code = db.Insert(userRole) > 0 ? 200 : 503;
                    result.Message = "角色绑定成功！";
                }
                UserModel user = db.GetEntity<UserModel>(ent.UserId);
                user.ModifyTime = DateTime.Now;
                user.ModifyPerson = OperatorProvider.Provider.GetCurrent().UserName;
                db.Update<UserModel>(user);
                return result;
            });
            int i = _context.UsingTransaction<AjaxResultEnt>(func);
            if (i > 0)
                return result;
            else
            {
                result.Code = 404;
                result.Message = "角色绑定失败！";
                return result;
            }
        }
    }
}
