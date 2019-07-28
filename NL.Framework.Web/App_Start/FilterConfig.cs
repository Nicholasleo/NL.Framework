using NL.Framework.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace NL.Framework.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new NLFrameActionFilterAttribute());
            filters.Add(new NLFrameAuthenticationAttribute());
            filters.Add(new NLFrameAuthorizeAttribute());
            filters.Add(new NLFrameHandleErrorAttribute());
        }
    }
}
