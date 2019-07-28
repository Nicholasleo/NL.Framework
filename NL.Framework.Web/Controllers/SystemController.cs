using NL.Framework.IDAL;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public partial class SystemController : Controller
    {
        private readonly IDbContext _context;

        public SystemController(IDbContext db)
        {
            _context = db;
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