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
        }
    }
}
