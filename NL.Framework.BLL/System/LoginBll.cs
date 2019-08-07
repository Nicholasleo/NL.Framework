﻿//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-07 13:44:43
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Common.Cache;
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.BLL
{
    public class LoginBll : ILoginBll
    {

        private readonly IDbContext _context;
        public LoginBll(IDbContext db)
        {
            _context = db;
        }

        public LoginStatusEnt CheckUserLogin(LoginEnt loginEnt)
        {
            LoginStatusEnt res = new LoginStatusEnt();
            if (string.IsNullOrEmpty(loginEnt.UserCode))
            {
                res.Code = 404;
                res.Message = "用户名不能为空！";
                return res;
            }
            if (string.IsNullOrEmpty(loginEnt.Password))
            {
                res.Code = 404;
                res.Message = "密码不能为空！";
                return res;
            }

            UserModel userModel = _context.GetEntity<UserModel>(t => t.UserCode.ToLower().Equals(loginEnt.UserCode.ToLower()) && t.UserPwd.Equals(loginEnt.Password));
            if (userModel != null)
            {
                res.Code = 200;
                res.Message = "登录成功！";
                var result = from u in _context.Set<UserModel>()
                             join ur in _context.Set<UserRoleModel>()
                             on u.Fid equals ur.UserId
                             join r in _context.Set<RoleModel>()
                             on ur.RoleId equals r.Fid
                             where u.UserCode.ToLower().Equals(loginEnt.UserCode) && u.UserPwd.Equals(loginEnt.Password)
                             select new
                             {
                                 RoleId = r.Fid,
                                 RoleCode = r.RoleCode,
                                 RoleName = r.RoleName,
                                 UserId = u.Fid,
                                 UserName = u.UserName,
                                 UserCode = u.UserCode,
                                 UserPwd = u.UserPwd
                             };

                if (result == null || result.ToList().Count <= 0)
                {
                    res.Code = 404;
                    res.Message = "用户未授权角色，请联系系统管理员！";
                    return res;
                }
                foreach (var item in result.AsQueryable())
                {
                    LoginUserEnt ent = new LoginUserEnt();
                    ent.RoleId = item.RoleId;
                    ent.RoleCode = item.RoleCode;
                    ent.RoleName = item.RoleName;
                    ent.UserId = item.UserId;
                    ent.UserName = item.UserName;
                    ent.UserCode = item.UserCode;
                    ent.UserPwd = item.UserPwd;
                    res.LoginUserEnt = ent;
                }
                Session.SetCookie("NLFRAME_LOGIN_TOKEN", res.LoginUserEnt);
                return res;
            }
            else
            {
                userModel = _context.GetEntity<UserModel>(t => t.UserCode.ToLower().Equals(loginEnt.UserCode.ToLower()));
                if (userModel == null)
                {
                    res.Code = 404;
                    res.Message = "用户名错误！";
                    return res;
                }
                userModel = _context.GetEntity<UserModel>(t => t.UserCode.ToLower().Equals(loginEnt.UserCode.ToLower()) && t.UserPwd.Equals(loginEnt.Password));
                if (userModel == null)
                {
                    res.Code = 404;
                    res.Message = "密码错误！";
                    return res;
                }

                res.Code = 503;
                res.Message = "登录异常，请联系管理员！";
                return res;
            }
        }
    }
}
