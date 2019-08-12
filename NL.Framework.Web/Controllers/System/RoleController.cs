using Newtonsoft.Json;
using NL.Framework.Common;
using NL.Framework.Model;
using NL.Framework.Model.System;
using NL.Framework.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public class PageModels
    {
        public IQueryable RoleLists { get; set; }
        public IQueryable MenuLists { get; set; }
        public IQueryable FunctionLists { get; set; }
    }
    public partial class SystemController
    {
        public ActionResult RoleIndex(Guid id)
        {
            PageModels model = new PageModels();
            model.RoleLists = _IRoleBll.GetQueryable();
            model.FunctionLists = _IRoleBll.GetMenuFunction(id, ent.RoleId).AsQueryable();
            return View(model);
        }

        public ActionResult RoleAdd()
        {
            return View();
        }

        public ActionResult RoleEdit(Guid fid)
        {
            RoleModel model = _IRoleBll.GetModel(fid);
            return View(model);
        }


        [HttpGet]
        public JsonResult GetRoleList(int page, int limit, string role = "")
        {
            AjaxResultData<RoleModel> result = new AjaxResultData<RoleModel>();
            int total = 0;
            List<RoleModel> data = _IRoleBll.GetLists(page, limit, out total, role);
            string json = JsonConvert.SerializeObject(data);
            result.data = json;
            result.count = total.ToString();
            result.code = 0;
            result.msg = "";
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRole(List<RoleModel> model)
        {
            resData = _IRoleBll.Delete(model);
            return Json(resData);
        }

        [HttpPost]
        public JsonResult UpdateRole(RoleModel model)
        {
            resData = _IRoleBll.Update(model);
            return Json(resData);
        }

        [HttpPost]
        public JsonResult AddRole(RoleModel model)
        {
            resData  = _IRoleBll.Create(model);
            return Json(resData);
        }
    }
}