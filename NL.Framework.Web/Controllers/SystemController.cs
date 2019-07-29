using NL.Framework.IBLL;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public partial class SystemController : Controller
    {
        private readonly IRoleBll _IRoleBll;

        public SystemController(IRoleBll roleBll)
        {
            _IRoleBll = roleBll;
        }

        public ActionResult UserIndex()
        {
            return View();
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