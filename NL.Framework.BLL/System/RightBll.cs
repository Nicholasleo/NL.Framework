//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-06 15:14:08
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
    public class RightBll : IRightBll
    {
        private readonly IDbContext _context;
        public RightBll(IDbContext db)
        {
            _context = db;
        }
        public List<TreeBaseEnt> GetTreeLists(Guid roleFid)
        {
            List<TreeBaseEnt> lists = new List<TreeBaseEnt>();
            try
            {
                //获取所有的功能
                IQueryable functions = _context.GetLists<FunctionModel>();
                //获取所有的菜单
                //IQueryable menus = _context.GetLists<MenuModel>();
                //获取所有根节点
                IQueryable roots = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(Guid.Empty));
                //获取角色菜单关系
                IQueryable roleMenuModels = _context.GetLists<RoleMenuModel>(t => t.RoleId.Equals(roleFid));
                //获取菜单功能关系
                List<object> rightList = new List<object>();
                Dictionary<Guid, List<Guid>> _dir = new Dictionary<Guid, List<Guid>>();
                foreach (RoleMenuModel item in roleMenuModels)
                {
                    IQueryable temp = _context.GetLists<RoleMenuFunctionModel>(t => t.RoleMenuId.Equals(item.Fid));
                    List<Guid> funcList = new List<Guid>();
                    foreach (RoleMenuFunctionModel r in temp)
                    {
                        funcList.Add(r.FunctionId);
                    }
                    if (_dir.ContainsKey(item.MenuId))
                    {
                        _dir[item.MenuId] = funcList;
                    }
                    _dir.Add(item.MenuId, funcList);
                }

                //将所有的子菜单添加上功能
                foreach (MenuModel root in roots)
                {
                    TreeBaseEnt treeData = new TreeBaseEnt();
                    treeData.Id = root.Fid;
                    treeData.Name = root.MenuName;
                    treeData.Disabled = root.MenuIsShow > 0;
                    List<TreeBaseEnt> childs = new List<TreeBaseEnt>();
                    IQueryable menus = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(root.Fid));
                    //子菜单
                    foreach (MenuModel menu in menus)
                    {
                        TreeBaseEnt m = new TreeBaseEnt();
                        m.Id = menu.Fid;
                        m.Name = menu.MenuName;
                        m.Disabled = menu.MenuIsShow > 0;
                        List<TreeBaseEnt> mChilds = new List<TreeBaseEnt>();
                        try
                        {
                            List<Guid> _fun = new List<Guid>();
                            if (_dir.ContainsKey(menu.Fid))
                            {
                                _fun = _dir.First(t => t.Key.Equals(menu.Fid)).Value;
                            }
                            foreach (FunctionModel func in functions)
                            {
                                TreeBaseEnt baseEnt = new TreeBaseEnt();
                                baseEnt.Id = func.Fid;
                                baseEnt.Name = func.FunctionName;
                                if (_fun != null && _fun.Count > 0)
                                {
                                    baseEnt.IsChecked = _fun.Contains(func.Fid);
                                }
                                mChilds.Add(baseEnt);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        m.Childrens = mChilds;
                        childs.Add(m);
                    }
                    treeData.Childrens = childs;
                    lists.Add(treeData);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lists;
        }
    }
}
