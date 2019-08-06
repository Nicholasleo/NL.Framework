using Newtonsoft.Json;
using NL.Framework.IBLL;
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

        public SystemController(IRoleBll roleBll
            , IUserBll userBll
            , IRightBll rightBll
            , IMenuBll menuBll)
        {
            _IRoleBll = roleBll;
            _IUserBll = userBll;
            _IRightBll = rightBll;
            _IMenuBll = menuBll;
        }


        public ActionResult RightIndex()
        {
            return View();
        }
    }
}