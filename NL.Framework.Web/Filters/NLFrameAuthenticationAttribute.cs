using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-27 14:20:07 
//    说明：
//    版权所有：个人
//***********************************************************
namespace NL.Framework.Web.Filters
{
    public class NLFrameAuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        /// <summary>
        /// 对请求进行身份验证
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 在ActionResult的ExecuteResult执行之前运行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //throw new NotImplementedException();
        }
    }
}
