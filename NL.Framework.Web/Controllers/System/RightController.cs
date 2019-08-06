using Newtonsoft.Json;
using NL.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public partial class SystemController
    {
        // GET: Right
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetMenuFuncTree(Guid fid)
        {

            List<TreeBaseEnt> result = _IRightBll.GetTreeLists(fid);
            string json = JsonConvert.SerializeObject(result);
            return Json(new { code=0,data=json }, JsonRequestBehavior.AllowGet);
        }
    }
}