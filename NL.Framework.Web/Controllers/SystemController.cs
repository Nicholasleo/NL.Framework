using NL.Framework.Common;
using NL.Framework.IBLL;
using NL.Framework.Model;
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
    public partial class SystemController : Controller
    {
        private readonly IRoleBll _IRoleBll;
        private readonly IUserBll _IUserBll;
        private readonly IMenuBll _IMenuBll;
        private readonly IRightBll _IRightBll;
        private readonly IDropdownBll _IDropdownBll;

        private static LoginUserEnt ent;

        private static AjaxResultEnt resData = new AjaxResultEnt();
        private IQueryable _ParentMenuList;
        public SystemController(IRoleBll roleBll
            , IUserBll userBll
            , IRightBll rightBll
            , IMenuBll menuBll
            , IDropdownBll dropdownBll)
        {
            _IDropdownBll = dropdownBll;
            _IRoleBll = roleBll;
            _IUserBll = userBll;
            _IRightBll = rightBll;
            _IMenuBll = menuBll;


            ent = OperatorProvider.Provider.GetCurrent();
        }

        internal IQueryable ParentMenuList {
            get {
                if(this._ParentMenuList == null)
                    this._ParentMenuList = _IMenuBll.GetQueryable();
                return this._ParentMenuList;
            }
        }


    }
}