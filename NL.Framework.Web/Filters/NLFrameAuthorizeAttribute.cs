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
            //throw new NotImplementedException();
            //if (Ignore)
            //{
            //    return;
            //}
            //try
            //{
            //    var token = HttpContext.Current.Request.Cookies["NLFRAME_LOGIN_TOKEN"];
            //    if (token == null)
            //    {
            //        filterContext.Result = new RedirectResult("/Login/Index");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    filterContext.Result = new RedirectResult("/Login/Index");
            //    throw;
            //}
        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    //if (httpContext.Session["UserInfo"] == null)
        //    //{
        //    //    return false;
        //    //}
        //    return true;
        //}

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    //如果是Ajax请求
        //    if (filterContext.HttpContext.Request.IsAjaxRequest())
        //    {
        //        //filterContext.Result = new JsonResult
        //        //{
        //        //    //Data = new
        //        //    //{
        //        //    //    ResultCode = ResultCode.Exception,
        //        //    //    ResultMess = "请求用户未登录！"
        //        //    //}
        //        //};
        //    }
        //    else
        //    {
        //        //处理Url请求
        //        //验证不通过,直接跳转到相应页面，注意：如果不使用以下跳转，则会继续执行Action方法 
        //        //filterContext.Result = new RedirectResult("/Home/Index");
        //    }
        //}
    }
}
