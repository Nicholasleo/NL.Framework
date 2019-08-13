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
            TreeDataEnt resultDt = new TreeDataEnt();
            List<RightTreeBaseEnt> result = _IRightBll.GetTreeLists(fid);
            resultDt.TreeData = result;
            if (result != null && result.Count > 0)
            {
                resultDt.DataStatus = new TreeDataStatusEnt
                {
                    Code = "200",
                    Message = "获取数据成功！"
                };
            }
            string json = JsonConvert.SerializeObject(resultDt);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveRightInfo(RightSaveEnt data)
        {
            resData = _IRightBll.SaveRoleRight(data);
            return Json(resData);
        }
    }
}