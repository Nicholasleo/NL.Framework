//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-06 15:14:08
//    说明：
//    版权所有：个人
//***********************************************************
using Newtonsoft.Json;
using NL.Framework.Common;
using NL.Framework.Common.Log;
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NL.Framework.BLL
{
    public class RightBll : IRightBll
    {
        private readonly IDbContext _context;
        private readonly ILogger _ILogger;
        public RightBll(IDbContext db,ILogger logger)
        {
            _ILogger = logger;
            _context = db;
        }
        public List<RightTreeBaseEnt> GetTreeLists(Guid roleFid)
        {
            List<RightTreeBaseEnt> lists = new List<RightTreeBaseEnt>();

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
                    List<string> _flgList = new List<string>();
                    RightTreeBaseEnt treeData = new RightTreeBaseEnt();
                    string _status = "0";
                    treeData.Id = root.Fid;
                    treeData.Name = root.MenuName;
                    treeData.ParentId = root.MenuParentId;
                    List<RightTreeBaseEnt> childs = new List<RightTreeBaseEnt>();
                    IQueryable menus = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(root.Fid));
                    //子菜单
                    foreach (MenuModel menu in menus)
                    {
                        RightTreeBaseEnt m = new RightTreeBaseEnt();
                        m.Id = menu.Fid;
                        m.Name = menu.MenuName;
                        m.ParentId = menu.MenuParentId;
                        List<RightTreeBaseEnt> mChilds = new List<RightTreeBaseEnt>();
                        int funcNum = 0;
                        List<Guid> _fun = new List<Guid>();
                        try
                        {
                            if (_dir.ContainsKey(menu.Fid))
                            {
                                _fun = _dir.First(t => t.Key.Equals(menu.Fid)).Value;
                            }
                            foreach (FunctionModel func in functions)
                            {
                                funcNum++;
                                RightTreeBaseEnt baseEnt = new RightTreeBaseEnt();
                                baseEnt.Id = func.Fid;
                                baseEnt.Name = func.FunctionName;
                                baseEnt.ParentId = menu.Fid;
                                baseEnt.Last = true;
                                baseEnt.CheckArrs = new List<CheckArr> {
                                    new CheckArr()
                                };
                                if (_fun != null && _fun.Contains(func.Fid))
                                {
                                    List<CheckArr> checkArrs = new List<CheckArr> {
                                        new CheckArr("1")
                                    };
                                    baseEnt.CheckArrs = checkArrs;
                                }
                                mChilds.Add(baseEnt);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        _status = GetCheckStatus(funcNum, _fun.Count);
                        if(!_flgList.Contains(_status))
                            _flgList.Add(_status);
                        m.CheckArrs = new List<CheckArr> {
                            new CheckArr(_status)
                        };
                        m.Childrens = mChilds;
                        m.Leaf = mChilds.Count > 0;
                        childs.Add(m);
                    }
                    treeData.CheckArrs = new List<CheckArr> {
                        new CheckArr(GetCheckStatus(_flgList))
                    };
                    treeData.Childrens = childs;
                    treeData.Leaf = childs.Count > 0;
                    lists.Add(treeData);

                }
            }
            catch (Exception ex)
            {
                _ILogger.Error("获取权限树异常", ex);
                throw new Exception(ex.Message);
            }
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"获取菜单列表：{JsonConvert.SerializeObject(lists)}");
            }
            return lists;
        }

        private List<RightTreeBaseEnt> GetChildNodes(Guid id)
        {
            List<RightTreeBaseEnt> list = new List<RightTreeBaseEnt>();
            IQueryable nodes = _context.GetLists<MenuModel>(t => t.MenuParentId.Equals(id));
            if (nodes != null)
            {
                foreach (MenuModel node in nodes)
                {
                    RightTreeBaseEnt treeData = new RightTreeBaseEnt();
                    treeData.Id = node.Fid;
                    treeData.Name = node.MenuName;
                    treeData.ParentId = node.MenuParentId;
                    treeData.Childrens = GetChildNodes(node.Fid);
                    list.Add(treeData);
                }
            }
            return list;
        }


        /// <summary>
        /// 0 1 2
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        private string GetCheckStatus(List<string> l)
        {
            if (l.Count > 1)
                return "2";
            else
            {
                return l[0];
            }
        }
        private string GetCheckStatus(int f, int l)
        {
            if (f == l)
                return "1";
            if (f > l && l > 0)
                return "2";
            if (l == 0)
                return "0";
            return "0";
        }



        public AjaxResultEnt SaveRoleRight(RightSaveEnt data)
        {
            AjaxResultEnt result = new AjaxResultEnt();
            Action<IDbContext> action = new Action<IDbContext>((IDbContext db) => {
                Guid roleId = data.RoleId;
                List<RoleMenuModel> roleMenus = new List<RoleMenuModel>();
                //先删除功能授权关系表数据
                IQueryable res = db.GetLists<RoleMenuModel>(t => t.RoleId.Equals(roleId));
                foreach (RoleMenuModel item in res)
                {
                    if (db.IsExist<RoleMenuFunctionModel>(t => t.RoleMenuId.Equals(item.Fid)))
                        db.Delete<RoleMenuFunctionModel>(t => t.RoleMenuId.Equals(item.Fid));
                }
                //再删除角色菜单授权关系表数据
                if (db.IsExist<RoleMenuModel>(t => t.RoleId.Equals(roleId)))
                    db.Delete<RoleMenuModel>(t => t.RoleId.Equals(roleId));

                if (data.RoleMenuEnts != null && data.RoleMenuEnts.Count > 0)
                {
                    foreach (var item in data.RoleMenuEnts)
                    {
                        RoleMenuModel model = new RoleMenuModel();
                        model.RoleId = roleId;
                        model.MenuId = item.MenuId;
                        model.CreatePerson = OperatorProvider.Provider.GetCurrent().UserName;
                        model.CreateTime = DateTime.Now;
                        roleMenus.Add(model);
                    }
                    //新增角色菜单关系
                    db.Insert<RoleMenuModel>(roleMenus);

                    List<RoleMenuFunctionModel> roleMenuFunctions = new List<RoleMenuFunctionModel>();
                    //新增角色菜单功能关系
                    foreach (var item in data.RoleMenuFunctionEnts)
                    {
                        //获取角色菜单关系主键
                        RoleMenuModel ent = _context.GetEntity<RoleMenuModel>(t => t.RoleId.Equals(roleId) && t.MenuId.Equals(item.MenuId));
                        RoleMenuFunctionModel model = new RoleMenuFunctionModel();
                        model.RoleMenuId = ent.Fid;
                        model.FunctionId = item.FunctionId;
                        model.CreatePerson = OperatorProvider.Provider.GetCurrent().UserName;
                        model.CreateTime = DateTime.Now;
                        roleMenuFunctions.Add(model);
                    }
                    db.Insert<RoleMenuFunctionModel>(roleMenuFunctions);
                }
            });
            if (OperatorProvider.Provider.IsDebug)
            {
                _ILogger.Debug($"角色授权：{JsonConvert.SerializeObject(data)}");
            }
            int i = _context.UsingTransaction(action);
            if (i > 0)
            {
                result.Code = 200;
                result.Message = "角色授权成功!";
            }
            else
            {
                result.Code = 503;
                result.Message = "角色授权失败!";
            }
            return result;
        }
    }
}
