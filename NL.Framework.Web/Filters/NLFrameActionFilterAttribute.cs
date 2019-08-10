using NL.Framework.Common;
using NL.Framework.Common.Cache;
using NL.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-27 14:16:43 
//    说明：在Action方法执行之前和Action方法执行之后, 会执行此过滤器中的代码
//    版权所有：个人
//***********************************************************
namespace NL.Framework.Web.Filters
{
    public class NLFrameActionFilterAttribute : ActionFilterAttribute
    {
        public bool Ignore { get; set; }

        public NLFrameActionFilterAttribute(bool ignore = false)
        {
            Ignore = ignore;
        }

        public string Name
        {
            get; set;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
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
