using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public string Name
        {
            get; set;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            filterContext.HttpContext.Response.Write("我是执行前打出来的" + Name);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            filterContext.HttpContext.Response.Write("我是执行后打出来的" + Name);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            filterContext.HttpContext.Response.Write("我是在结果执行前打出来的" + Name);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            filterContext.HttpContext.Response.Write("我是结果执行后打出来的" + Name);
        }
    }
}
