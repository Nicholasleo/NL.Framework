using Newtonsoft.Json;
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public partial class SystemController
    {
        // GET: DropDown
        public ActionResult DropDown(Guid id)
        {
            PageModels model = new PageModels();
            model.FunctionLists = _IMenuBll.GetMenuFunction(id, ent.RoleId).AsQueryable();
            model.MenuLists = this.ParentMenuList;
            return View(model);
        }

        [HttpGet]
        public JsonResult GetDropDownList(int page,int limit,string filtter = "")
        {
            AjaxResultData<DropDownOptionsModel> result = new AjaxResultData<DropDownOptionsModel>();
            int total = 0;
            List<DropDownOptionsModel> data = _IDropdownBll.GetLists(page, limit, out total, filtter);
            string json = JsonConvert.SerializeObject(data);
            result.data = json;
            result.count = total.ToString();
            result.code = 0;
            result.msg = "";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDropDownTree(Guid fid)
        {
            DropdownTreeEnt resultDt = new DropdownTreeEnt();
            List<BaseTreeEnt> result = _IDropdownBll.GetTreeLists(fid);
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
    }
}