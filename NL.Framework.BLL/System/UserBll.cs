using Newtonsoft.Json;
using NL.Framework.Common;
using NL.Framework.Common.Config;
using NL.Framework.Common.Log;
using NL.Framework.Common.Security;
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model;
using NL.Framework.Model.NLFrameEnt;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;

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
    public class UserBll : CommonBll<UserModel>,IUserBll
    {

        #region Fields
        private readonly IDbContext _context;
        private readonly ILogger _ILogger;
        private static AjaxResultEnt result = new AjaxResultEnt
        {
            Code = 404,
            Message = "用户操作错误！"
        };
        #endregion

        #region Ctor
        public UserBll(IDbContext db, ILogger logger) : base(db, logger)
        {
            _ILogger = logger;
            _context = db;
        }
        #endregion

        #region Override
        public override List<UserModel> GetLists(int page, int limit, out int total, object obj)
        {
            UserPageEnt pageEnt = obj as UserPageEnt;
            Expression<Func<UserModel, bool>> where = null;
            if (!string.IsNullOrEmpty(pageEnt.UserCode))
            {
                if (where == null)
                    where = t => t.UserCode.ToLower().Contains(pageEnt.UserCode.ToLower());
                else
                    where = ExpressionHelp.ExpressionAnd(where, where);
            }
            if (!string.IsNullOrEmpty(pageEnt.UserName))
            {
                if (where == null)
                    where = t => t.UserName.ToLower().Contains(pageEnt.UserName.ToLower());
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
            if (!string.IsNullOrEmpty(pageEnt.IdCard))
            {
                if (where == null)
                    where = t => t.IdCard.ToLower().Contains(pageEnt.IdCard.ToLower());
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
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取用户列表（分页）：{JsonConvert.SerializeObject(result)}");
            }
            return result;
        }
        public override AjaxResultEnt Delete(Guid fid)
        {
            UserModel model = _context.GetEntity<UserModel>(fid);
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"删除用户：{JsonConvert.SerializeObject(model)}");
            }
            if (model.UserCode.ToLower().Equals("admin") || model.UserCode.ToLower().Equals("nicholasleo"))
            {
                result.Code = 400;
                result.Message = "系统超级管理员用户无法删除!";
                return result;
            }
            int i = _context.Delete<UserModel>(fid);
            if (i > 0)
            {
                result.Code = 200;
                result.Message = $"删除用户【{model.UserName}】成功!";
            }
            else
            {
                result.Code = 503;
                result.Message = $"用户【{model.UserName}】删除失败!";
            }
            return result;
        }
        public override IQueryable GetQueryable()
        {
            IQueryable data = _context.GetLists<UserModel>();
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取全部用户：{JsonConvert.SerializeObject(data)}");
            }
            return data;
        }
        public override UserModel GetModel(Guid fid)
        {
            UserModel model = _context.GetEntity<UserModel>(fid);
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取用户实体：{JsonConvert.SerializeObject(model)}");
            }
            return model;
        }
        public override AjaxResultEnt Delete(List<UserModel> users)
        {
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"删除用户：{JsonConvert.SerializeObject(users)}");
            }
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
                    if (i > 0)
                        username += model.UserName + ",";
                }
            });

            int state = _context.UsingTransaction(action) > 0 ? 200 : 404;
            result.Code = state;
            if (state == 200)
                result.Message = $"删除【{username.TrimEnd(',')}】成功！";

            return result;
        }
        public override List<FunctionModel> GetMenuFunction()
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
        public AjaxResultEnt AddUser(UserEditEnt model)
        {
            AjaxResultEnt result = new AjaxResultEnt();
            result.Code = 100;
            Action<IDbContext> action = new Action<IDbContext>((IDbContext db) => {
                if (db.IsExist<UserModel>(t => t.UserCode.ToLower().Equals(model.UserCode)))
                {
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
                    CreatePerson = OperatorProvider.Provider.GetCurrent().UserName,
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
                    CreatePerson = OperatorProvider.Provider.GetCurrent().UserName,
                    CreateTime = DateTime.Now
                };
                db.Insert<UserRoleModel>(userRole);
                if (!string.IsNullOrEmpty(model.ImageUrl))
                {
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string imagePath = Configs.GetValue(SystemParameters.NLFRAME_SYSTEM_CONFIG_UPLOAD_USER);
                    UserImageModel img = new UserImageModel
                    {
                        ImageUrl = model.ImageUrl,
                        Fid = userId,
                        UserIcon = ImageHelper.ConvertToByte($"{basePath}{imagePath}{model.ImageUrl}"),
                        CreatePerson = OperatorProvider.Provider.GetCurrent().UserName,
                        CreateTime = DateTime.Now
                    };
                    db.Insert(img);
                }
            });
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"添加用户：{JsonConvert.SerializeObject(model)}");
            }
            int flg = _context.UsingTransaction(action);
            if (result.Code == 100 && flg > 0)
            {
                result.Code = 200;
                result.Message = "用户添加成功！";
            }
            return result;
        }

        public UserEditEnt GetUserEidtModel(Guid fid)
        {
            UserEditEnt ent = new UserEditEnt();
            Guid roleid = Guid.Empty;
            UserModel model = _context.GetEntity<UserModel>(fid);
            UserRoleModel uModel = _context.GetEntity<UserRoleModel>(t => t.UserId.Equals(fid));
            UserImageModel img = _context.GetEntity<UserImageModel>(fid);
            string imgName = "";
            if (uModel != null)
                roleid = uModel.RoleId;
            if (img != null)
                imgName = img.ImageUrl;
            ent = new UserEditEnt
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
                ImageUrl = imgName,
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
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取用户实体：{JsonConvert.SerializeObject(ent)}");
            }
            return ent;
        }

        public UserRoleModel GetUserRoleModel(Guid fid)
        {
            UserRoleModel ent = _context.GetEntity<UserRoleModel>(t => t.UserId.Equals(fid));
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取用户角色实体：{JsonConvert.SerializeObject(ent)}");
            }
            return ent;
        }

        public AjaxResultEnt UpdateUser(UserEditEnt model)
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
                    if (OperatorProvider.Provider.IsDebug)
                    {
                        _ILogger.Debug($"用户修改：{JsonConvert.SerializeObject(userModel)}");
                    }
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

                if (!string.IsNullOrEmpty(model.ImageUrl))
                {
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string imagePath = Configs.GetValue(SystemParameters.NLFRAME_SYSTEM_CONFIG_UPLOAD_USER);
                    UserImageModel img = new UserImageModel
                    {
                        ImageUrl = model.ImageUrl,
                        Fid = model.Fid,
                        UserIcon = ImageHelper.ConvertToByte($"{basePath}{imagePath}{model.ImageUrl}")
                    };
                    if (db.IsExist<UserImageModel>(model.Fid))
                    {
                        UserImageModel temp = db.GetEntity<UserImageModel>(model.Fid);
                        temp.ImageUrl = img.ImageUrl;
                        temp.UserIcon = img.UserIcon;
                        temp.ModifyPerson = OperatorProvider.Provider.GetCurrent().UserName;
                        temp.ModifyTime = DateTime.Now;
                        db.Update<UserImageModel>(temp);
                    }
                    else
                    {
                        img.CreatePerson = OperatorProvider.Provider.GetCurrent().UserName;
                        img.CreateTime = DateTime.Now;
                        db.Insert(img);
                    }
                }
            });
            int i = _context.UsingTransaction(action);
            if (i > 0)
            {
                result.Code = 200;
                result.Message = "修改用户成功!";
            }
            else
            {
                result.Code = 503;
                result.Message = "修改用户失败!";
            }
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
                if (OperatorProvider.Provider.IsDebug)
                {
                    _ILogger.Debug($"修改用户角色绑定：{JsonConvert.SerializeObject(ent)}");
                }
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

        public UploadFileEnt UploadImage(HttpFileCollectionBase fileInfo)
        {
            UploadFileEnt resData = new UploadFileEnt();
            HttpPostedFileBase _file = fileInfo["File"];
            string file = _file.FileName;
            string fileFormat = file.Split('.')[file.Split('.').Length - 1]; // 以“.”截取，获取“.”后面的文件后缀
            Regex imageFormat = new Regex(@"^(bmp)|(png)|(gif)|(jpg)|(jpeg)"); // 验证文件后缀的表达式（自己写的，不规范别介意哈）
            if (string.IsNullOrEmpty(file) || !imageFormat.IsMatch(fileFormat)) // 验证后缀，判断文件是否是所要上传的格式
            {
                resData.ResultState = 503;
                resData.ResultMsg = "error";
            }
            else
            {
                string timeStamp = DateTime.Now.Ticks.ToString(); // 获取当前时间的string类型
                //string firstFileName = timeStamp.Substring(0, timeStamp.Length - 4); // 通过截取获得文件名
                string firstFileName = Md5.MD5Encrypt(OperatorProvider.Provider.GetCurrent().UserCode);
                string imageStr = Configs.GetValue(SystemParameters.NLFRAME_SYSTEM_CONFIG_UPLOAD_USER); // 获取保存图片的项目文件夹
                string uploadPath = $"{AppDomain.CurrentDomain.BaseDirectory}{imageStr}"; // 将项目路径与文件夹合并
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);
                string pictureFormat = file.Split('.')[file.Split('.').Length - 1];// 设置文件格式
                string fileName = firstFileName + "." + fileFormat;// 设置完整（文件名+文件格式） 
                string saveFile = uploadPath + fileName;//文件路径
                if (File.Exists(saveFile))
                    File.Delete(saveFile);
                _file.SaveAs(saveFile);// 保存文件
                // 如果单单是上传，不用保存路径的话，下面这行代码就不需要写了！
                resData.ResultPath = fileName;// 设置数据库保存的路径
                resData.ResultState = 200;
                resData.ResultMsg = "success";
            }
            return resData;
        }

        #endregion

        #region Methods
        #endregion
    }
}
