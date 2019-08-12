using Newtonsoft.Json;
using NL.Framework.Common;
using NL.Framework.Model;
using NL.Framework.Model.NLFrameEnt;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NL.Framework.Web.Controllers
{
    public class PageUserEditEnt
    {
        public UserEditEnt UserModel { get; set; }

        public List<RoleModel> RoleModels { get; set; }
    }

    public partial class SystemController
    {
        private static List<RoleModel> _RoleLists = null;
        public ActionResult UserIndex(Guid id)
        {
            if (_RoleLists == null || _RoleLists.Count <= 0)
                _RoleLists = _IRoleBll.GetLists();
            PageModels model = new PageModels();
            model.FunctionLists = _IUserBll.GetMenuFunction(id, ent.RoleId).AsQueryable();
            return View(model);
        }

        public ActionResult UserEdit(Guid fid)
        {
            UserEditEnt model = _IUserBll.GetUserEidtModel(fid);
            PageUserEditEnt ent = new PageUserEditEnt();
            ent.UserModel = model;
            ent.RoleModels = _RoleLists;
            return View(ent);
        }

        public ActionResult UserBind(Guid fid)
        {
            UserEditEnt model = _IUserBll.GetUserEidtModel(fid);
            PageUserEditEnt ent = new PageUserEditEnt();
            ent.UserModel = model;
            ent.RoleModels = _RoleLists;
            return View(ent);
        }

        public ActionResult UserAdd()
        {
            return View(_RoleLists);
        }

        [HttpGet]
        public JsonResult GetUserList(int page, int limit, string UserCode = "",string UserName = "",int Gender = 3,string Email="")
        {
            AjaxResultData<UserModel> result = new AjaxResultData<UserModel>();
            UserPageEnt pageEnt = new UserPageEnt
            {
                UserCode = UserCode,
                UserName = UserName,
                Gender = Gender,
                Email = Email
            };
            int total = 0;
            List<UserModel> data = _IUserBll.GetLists(page, limit, out total, pageEnt);
            string json = JsonConvert.SerializeObject(data);
            result.data = json;
            result.count = total.ToString();
            result.code = 0;
            result.msg = "";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteUser(List<UserModel> data)
        {
            resData = _IUserBll.Delete(data);
            return Json(resData);
        }

        [HttpPost]
        public JsonResult UpdateUser(UserEditEnt model)
        {
            resData = _IUserBll.UpdateUser(model);
            return Json(resData);
        }

        [HttpPost]
        public JsonResult AddUser(UserEditEnt model)
        {
            resData = _IUserBll.AddUser(model);
            return Json(resData);
        }

        [HttpPost]
        public JsonResult UpdateUserRole(UserRoleEnt data)
        {
            resData = _IUserBll.UpdateUserRole(data);
            return Json(resData);
        }
    }
}