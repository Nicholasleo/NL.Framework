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
            

        }
    }
}
