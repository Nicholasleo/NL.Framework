using NL.Framework.IBLL;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public partial class SystemController : Controller
    {
        private readonly IRoleBll _IRoleBll;
        private readonly IUserBll _IUserBll;

        public SystemController(IRoleBll roleBll
            , IUserBll userBll)
        {
            _IRoleBll = roleBll;
            _IUserBll = userBll;
        }


        public ActionResult RightIndex()
        {
            return View();
        }

        public ActionResult MenuIndex()
        {
            return View();
        }
    }
}