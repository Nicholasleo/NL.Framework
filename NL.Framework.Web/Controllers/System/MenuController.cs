using Newtonsoft.Json;
using NL.Framework.Common;
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
            model.MenuLists = _IMenuBll.GetQueryable();
            return View(model);
        }

        public ActionResult MenuEdit(Guid fid)
        {
            if (_ParentMenuList == null)
                _ParentMenuList = _IMenuBll.GetQueryable();
            MenuEditModel model = new MenuEditModel();
            //MenuModel model = new MenuModel();
            model.Menu = _IMenuBll.GetModel(fid);
            model.ParentMenuList = _ParentMenuList;
            return View(model);
        }

        public ActionResult MenuAdd()
        {
            IQueryable model = _IMenuBll.GetQueryable();
            return View(model);
        }

        [HttpGet]
        public JsonResult GetMenuList(int page, int limit, string filtter = "")
        {
            AjaxResultData<MenuModel> result = new AjaxResultData<MenuModel>();
            int total = 0;
            List<MenuModel> data = _IMenuBll.GetLists(page, limit, out total, filtter);
            string json = JsonConvert.SerializeObject(data);
            result.data = json;
            result.count = total.ToString();
            result.code = 0;
            result.msg = "";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMenu(List<MenuModel> model)
        {
            resData = _IMenuBll.Delete(model);
            return Json(resData);
        }

        [HttpPost]
        public JsonResult UpdateMenu(MenuModel model)
        {
            resData = _IMenuBll.Update(model);
            return Json(resData);
        }

        [HttpPost]
        public JsonResult AddMenu(MenuModel model)
        {
            resData = _IMenuBll.Create(model);
            return Json(resData);
        }
    }
}