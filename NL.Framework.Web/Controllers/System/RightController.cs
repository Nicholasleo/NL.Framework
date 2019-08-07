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
            List<TreeBaseEnt> result = _IRightBll.GetTreeLists(fid);
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
            ResultData result = new ResultData();
            int i = _IRightBll.SaveRoleRight(data);
            result.code = i;
            result.msg = i > 0 ? "授权成功！" : "授权失败！";
            result.data = new TokenData { access_token = Guid.NewGuid().ToString() };
            return Json(result);
        }
    }
}