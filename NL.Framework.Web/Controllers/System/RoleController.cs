using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public class PageModels
    {
        public IQueryable RoleLists { get; set; }
        public IQueryable FunctionLists { get; set; }
    }
    public partial class SystemController
    {

        public ActionResult RoleIndex()
        {
            PageModels model = new PageModels();
            model.RoleLists = _context.GetLists<RoleModel>();
            var menu = _context.GetEntity<MenuModel>(t => t.MenuName == "角色管理");
            var r = from f in _context.Set<FunctionModel>()
                    join fm in _context.Set<MenuFunctionModel>()
                    on f.Fid equals fm.FunctionId
                    join m in _context.Set<MenuModel>()
                    on fm.MenuId equals m.Fid
                    where m.Fid == menu.Fid
                    select new
                    {
                        FunctionName = f.FunctionName,
                        FunctionEvent = f.FunctionEvent
                    };
            List<FunctionModel> flist = new List<FunctionModel>();
            foreach (var item in r.ToList())
            {
                FunctionModel m = new FunctionModel();
                m.FunctionEvent = item.FunctionEvent;
                m.FunctionName = item.FunctionName;
                flist.Add(m);
            }
            model.FunctionLists = flist.AsQueryable();

            return View(model);
        }

        [HttpGet]
        public ActionResult GetRoleList(int page,int limit,string role = "")
        {
            AjaxResultData<RoleModel> result = new AjaxResultData<RoleModel>();
            int total = 0;
            Expression<Func<RoleModel, bool>> where = null;
            if (!string.IsNullOrEmpty(role))
                where = t => t.Fid.ToString() == role;
            IQueryable data = _context.GetLists<RoleModel>(page, limit, out total, where);
            result.data = new List<RoleModel>();
            foreach (RoleModel item in data)
            {
                result.data.Add(item);
            }
            result.count = total.ToString();
            result.code = 0;
            result.msg = "";
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteRole(RoleModel model)
        {
            ResultData result = new ResultData();
            int i = _context.Delete<RoleModel>(model.Fid);
            result.code = i;
            result.msg = i > 0 ? "删除成功！" : "删除失败！";
            result.data = new TokenData { access_token = Guid.NewGuid().ToString() };
            return Json(result);
        }
    }
}