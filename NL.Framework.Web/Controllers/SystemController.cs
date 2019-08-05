using NL.Framework.IBLL;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public partial class SystemController : Controller
    {
        private readonly IRoleBll _IRoleBll;
        private readonly IUserBll _IUserBll;
        private readonly IMenuBll _IMenuBll;

        public SystemController(IRoleBll roleBll
            , IUserBll userBll
            ,IMenuBll menuBll)
        {
            _IRoleBll = roleBll;
            _IUserBll = userBll;
            _IMenuBll = menuBll;
        }


        public ActionResult RightIndex()
        {
            return View();
        }
    }
}