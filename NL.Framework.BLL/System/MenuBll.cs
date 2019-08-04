//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:11:54
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.BLL
{
    public class MenuBll : IMenuBll
    {
        private readonly IDbContext _context;
        public MenuBll(IDbContext db)
        {
            _context = db;
        }

        public List<NvaMenus> GetMenuList()
        {
            IQueryable menus = _context.GetLists<MenuModel>();

            List<NvaMenus> menuList = new List<NvaMenus>();
            //获取根节点
            var roots = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(Guid.Empty));
            //通过根节点获取对应的子节点
            foreach (MenuModel root in roots)
            {
                NvaMenus nva = new NvaMenus();
                nva.MenuName = root.MenuName;
                nva.MenuUrl = root.MenuUrl;
                nva.MenuIcon = root.MenuIcon;
                nva.Fid = root.Fid;
                List<MenuModel> childMenus = new List<MenuModel>();
                var childs = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(root.Fid));
                foreach (MenuModel child in childs)
                {
                    MenuModel menu = new MenuModel();
                    menu.Fid = child.Fid;
                    menu.MenuParentId = child.MenuParentId;
                    menu.MenuName = child.MenuName;
                    menu.MenuUrl = child.MenuUrl;
                    childMenus.Add(menu);
                }
                nva.ChildMenus = childMenus;
                menuList.Add(nva);
            }
            return menuList;
        }
    }
}
