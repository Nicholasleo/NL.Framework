using NL.Framework.Common;
using NL.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-27 14:12:57 
//    说明：
//    版权所有：个人
//***********************************************************
namespace NL.Framework.Web.Filters
{
    /// <summary>
    /// 身份过滤器
    /// </summary>
    public class NLFrameAuthorizeAttribute : FilterAttribute,IAuthorizationFilter
    {
        public bool Ignore { get; set; }
        public NLFrameAuthorizeAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //filterContext.HttpContext.Response.Write("我是执行前打出来的" + Name);
            //string action = filterContext.RouteData.Values["action"].ToString();
            string controller = filterContext.RouteData.Values["controller"].ToString();
            //过滤掉登录页面，防止多重跳转死循环
            if (controller.ToLower() == "login")
            {
                return;
            }
            if (Ignore)
            {
                return;
            }
            LoginUserEnt tempToken = OperatorProvider.Provider.GetCurrent();
            if (tempToken == null || tempToken.RoleId.Equals(Guid.Empty) || tempToken.UserCode.Equals("") || tempToken.UserPwd.Equals("") || tempToken.UserId.Equals(Guid.Empty))
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult()
                    {
                        //Data = new ErrorModel(AppConfig.LoginPageUrl, EMsgStatus.登录超时20),
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }
    }
}
