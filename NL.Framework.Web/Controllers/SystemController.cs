using Newtonsoft.Json;
using NL.Framework.Common;
using NL.Framework.Common.Log;
using NL.Framework.IBLL;
using NL.Framework.Model;
using System;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public partial class SystemController : Controller
    {
        private readonly IRoleBll _IRoleBll;
        private readonly IUserBll _IUserBll;
        private readonly IMenuBll _IMenuBll;
        private readonly IRightBll _IRightBll;

        private static LoginUserEnt ent;

        private static AjaxResultEnt resData = new AjaxResultEnt();
        public SystemController(IRoleBll roleBll
            , IUserBll userBll
            , IRightBll rightBll
            , IMenuBll menuBll
            , ILogger logger)
        {
            _IRoleBll = roleBll;
            _IUserBll = userBll;
            _IRightBll = rightBll;
            _IMenuBll = menuBll;
            ent = OperatorProvider.Provider.GetCurrent();
        }


        public ActionResult RightIndex()
        {
            return View();
        }
    }
}