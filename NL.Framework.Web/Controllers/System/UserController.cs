using Newtonsoft.Json;
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
    public partial class SystemController
    {
        public ActionResult UserIndex()
        {
            PageModels model = new PageModels();
            model.FunctionLists = _IUserBll.GetMenuFunction().AsQueryable();
            return View(model);
        }

        public ActionResult UserEdit(Guid fid)
        {
            UserModel model = _IUserBll.GetUserModel(fid);
            return View(model);
        }

        public ActionResult UserAdd()
        {
            return View();
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
            List<UserModel> data = _IUserBll.GetUserLists(page, limit, out total, pageEnt);
            string json = JsonConvert.SerializeObject(data);
            result.data = json;
            result.count = total.ToString();
            result.code = 0;
            result.msg = "";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteUser(UserModel model)
        {
            ResultData result = new ResultData();
            int i =_IUserBll.DeleteUser(model);
            result.code = i;
            result.msg = i > 0 ? "删除成功！" : "删除失败！";
            result.data = new TokenData { access_token = Guid.NewGuid().ToString() };
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdateUser(UserModel model)
        {
            model.ModifyPerson = "NicholasLeo";
            model.ModifyTime = DateTime.Now;
            ResultData result = new ResultData();
            int i = _IUserBll.UpdateUser(model);
            result.code = i;
            result.msg = i > 0 ? "修改成功！" : "修改失败！";
            result.data = new TokenData { access_token = Guid.NewGuid().ToString() };
            return Json(result);
        }

        [HttpPost]
        public JsonResult AddUser(UserModel model)
        {
            model.CreateTime = DateTime.Now;
            model.CreatePerson = "NicholasLeo";
            ResultData result = new ResultData();
            int i = _IUserBll.AddUser(model);
            result.code = i;
            result.msg = i > 0 ? "添加成功！" : "添加失败！";
            result.data = new TokenData { access_token = Guid.NewGuid().ToString() };
            return Json(result);
        }
    }
}