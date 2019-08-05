//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:11:13
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.IBLL
{
    public interface IMenuBll : ISystemBaseBll, IBaseBll
    {
        List<NvaMenus> GetMenuList();
        IQueryable GetParentMenu();
        List<MenuModel> GetMenuLists(int page, int limit, out int total, string filtter = "");
        MenuModel GetMenuModel(Guid fid);
        int DeleteMenu(MenuModel model);
        int UpdateMenu(MenuModel model);
        int AddMenu(MenuModel model);
    }
}
