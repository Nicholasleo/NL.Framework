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
            model.MenuLists = _IMenuBll.GetQueryable();
            return View(model);
        }
    }
}