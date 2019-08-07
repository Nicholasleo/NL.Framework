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
    public class MenuEditModel
    {
        public MenuModel Menu { get; set; }
        public IQueryable ParentMenuList { get; set; }
    }

    public partial class SystemController
    {
        private static IQueryable _ParentMenuList = null;
        public ActionResult MenuIndex(Guid id)
        {
            PageModels model = new PageModels();
            model.FunctionLists = _IMenuBll.GetMenuFunction(id,ent.RoleId).AsQueryable();
            model.MenuLists = _IMenuBll.GetParentMenu();
            return View(model);
        }

        public ActionResult MenuEdit(Guid fid)
        {
            //if (_ParentMenuList == null)
                _ParentMenuList = _IMenuBll.GetParentMenu();
            MenuEditModel model = new MenuEditModel();
            //MenuModel model = new MenuModel();
            model.Menu = _IMenuBll.GetMenuModel(fid);
            model.ParentMenuList = _ParentMenuList;
            return View(model);
        }

        public ActionResult MenuAdd()
        {
            IQueryable model = _IMenuBll.GetParentMenu();
            return View(model);
        }

        [HttpGet]
        public JsonResult GetMenuList(int page, int limit, string filtter = "")
        {
            AjaxResultData<MenuModel> result = new AjaxResultData<MenuModel>();
            int total = 0;
            List<MenuModel> data = _IMenuBll.GetMenuLists(page, limit, out total, filtter);
            string json = JsonConvert.SerializeObject(data);
            result.data = json;
            result.count = total.ToString();
            result.code = 0;
            result.msg = "";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMenu(MenuModel model)
        {
            ResultData result = new ResultData();
            int i = _IMenuBll.DeleteMenu(model);
            result.code = i;
            result.msg = i > 0 ? "删除成功！" : "删除失败！";
            result.data = new TokenData { access_token = Guid.NewGuid().ToString() };
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateMenu(MenuModel model)
        {
            model.ModifyPerson = "NicholasLeo";
            model.ModifyTime = DateTime.Now;
            ResultData result = new ResultData();
            int i = _IMenuBll.UpdateMenu(model);
            result.code = i;
            result.msg = i > 0 ? "修改成功！" : "修改失败！";
            result.data = new TokenData { access_token = Guid.NewGuid().ToString() };
            return Json(result);
        }

        [HttpPost]
        public JsonResult AddMenu(MenuModel model)
        {
            model.CreateTime = DateTime.Now;
            model.CreatePerson = "NicholasLeo";
            ResultData result = new ResultData();
            int i = _IMenuBll.AddMenu(model);
            result.code = i;
            result.msg = i > 0 ? "添加成功！" : "添加失败！";
            result.data = new TokenData { access_token = Guid.NewGuid().ToString() };
            return Json(result);
        }
    }
}