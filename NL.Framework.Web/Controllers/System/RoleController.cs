using Newtonsoft.Json;
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
        public ActionResult RoleIndex()
        {
            PageModels model = new PageModels();
            model.RoleLists = _IRoleBll.GetRoleAll();
            model.FunctionLists = _IRoleBll.GetMenuFunction().AsQueryable();
            return View(model);
        }

        public ActionResult RoleAdd()
        {
            return View();
        }

        public ActionResult RoleEdit(Guid fid)
        {
            RoleModel model = _IRoleBll.GetRoleModel(fid);
            return View(model);
        }


        [HttpGet]
        public JsonResult GetRoleList(int page, int limit, string role = "")
        {
            AjaxResultData<RoleModel> result = new AjaxResultData<RoleModel>();
            int total = 0;
            List<RoleModel> data = _IRoleBll.GetRolesLists(page, limit, out total, role);
            string json = JsonConvert.SerializeObject(data);
            result.data = json;
            result.count = total.ToString();
            result.code = 0;
            result.msg = "";
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRole(RoleModel model)
        {
            ResultData result = new ResultData();
            int i = _IRoleBll.DeleteRole(model);
            result.code = i;
            result.msg = i > 0 ? "删除成功！" : "删除失败！";
            result.data = new TokenData { access_token = Guid.NewGuid().ToString() };
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateRole(RoleModel model)
        {
            model.ModifyPerson = "NicholasLeo";
            model.ModifyTime = DateTime.Now;
            ResultData result = new ResultData();
            int i = _IRoleBll.UpdateRole(model);
            result.code = i;
            result.msg = i > 0 ? "修改成功！" : "修改失败！";
            result.data = new TokenData { access_token = Guid.NewGuid().ToString() };
            return Json(result);
        }

        [HttpPost]
        public JsonResult AddRole(RoleModel model)
        {
            model.CreateTime = DateTime.Now;
            model.CreatePerson = "NicholasLeo";
            ResultData result = new ResultData();
            int i = _IRoleBll.AddRole(model);
            result.code = i;
            result.msg = i > 0 ? "添加成功！" : "添加失败！";
            result.data = new TokenData { access_token = Guid.NewGuid().ToString() };
            return Json(result);
        }
    }
}