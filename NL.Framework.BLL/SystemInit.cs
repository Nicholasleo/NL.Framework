//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:05:40
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Common.Json;
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NL.Framework.BLL
{
    public class SystemInit : ISystemInit
    {
        private readonly IDbContext _context;

        public SystemInit(IDbContext db)
        {
            _context = db;
        }

        public void InitFunction()
        {
            List<FunctionModel> list = new List<FunctionModel>
            {
               new FunctionModel{
                   FunctionName = "查看",
                    FunctionEvent = "view"
               },
               new FunctionModel{
                   FunctionName = "新增",
                    FunctionEvent = "add"
               },
               new FunctionModel{
                   FunctionName = "编辑",
                    FunctionEvent = "edit"
               },
               new FunctionModel{
                   FunctionName = "删除",
                    FunctionEvent = "delete"
               },
               new FunctionModel{
                   FunctionName = "导入",
                    FunctionEvent = "import"
               },
               new FunctionModel{
                   FunctionName = "导出",
                    FunctionEvent = "export"
               }
            };
            if(!_context.IsExist<FunctionModel>(t=>t.FunctionName.Equals("新增")))
                _context.Insert(list);
        }

        public void InitUser()
        {
            Action<IDbContext> action = new Action<IDbContext>((IDbContext db) => {

                var role = db.GetEntity<RoleModel>(t => t.RoleCode.Equals("SuperAdmin"));
                if (!db.IsExist<UserModel>(t => t.UserCode.Equals("admin")))
                {
                    UserModel user = new UserModel
                    {
                        UserName = "NicholasLeo",
                        UserCode = "admin",
                        UserPwd = "123456",
                        IdCard = "362330199010303750",
                        UserAge = 27,
                        Gender = 1,
                        CreatePerson = "NicholasLeo",
                        CreateTime = DateTime.Now,
                        WeChat = "nicholasleo1030",
                        QQ = "461183790",
                        IsAdmin = 1,
                        IsDelete = 0,
                        MobilePhone = "13158985896"
                    };
                    db.Insert<UserModel>(user);
                }
                if (!db.IsExist<UserModel>(t => t.UserCode.Equals("test")))
                {
                    UserModel user = new UserModel
                    {
                        UserName = "测试人员",
                        UserCode = "test",
                        UserPwd = "123456",
                        IdCard = "362330199010303751",
                        UserAge = 27,
                        Gender = 1,
                        CreatePerson = "NicholasLeo",
                        CreateTime = DateTime.Now,
                        WeChat = "nicholasleo1030",
                        QQ = "461183790",
                        IsAdmin = 1,
                        IsDelete = 0,
                        MobilePhone = "13158985896"
                    };
                    db.Insert<UserModel>(user);
                }
                if (!db.IsExist<UserModel>(t => t.UserCode.Equals("test1")))
                {
                    UserModel user = new UserModel
                    {
                        UserName = "测试人员1",
                        UserCode = "test1",
                        UserPwd = "123456",
                        IdCard = "362330199010304751",
                        UserAge = 27,
                        Gender = 1,
                        CreatePerson = "NicholasLeo",
                        CreateTime = DateTime.Now,
                        WeChat = "nicholasleo1030",
                        QQ = "461183790",
                        IsAdmin = 1,
                        IsDelete = 0,
                        MobilePhone = "13158985896"
                    };
                    db.Insert<UserModel>(user);
                }
                if (!db.IsExist<UserModel>(t => t.UserCode.Equals("test2")))
                {
                    UserModel user = new UserModel
                    {
                        UserName = "测试人员2",
                        UserCode = "test2",
                        UserPwd = "123456",
                        IdCard = "362330199010304752",
                        UserAge = 27,
                        Gender = 1,
                        CreatePerson = "NicholasLeo",
                        CreateTime = DateTime.Now,
                        WeChat = "nicholasleo1030",
                        QQ = "461183790",
                        IsAdmin = 1,
                        IsDelete = 0,
                        MobilePhone = "13158985896"
                    };
                    db.Insert<UserModel>(user);
                }
            });
            _context.UsingTransaction(action);
        }


        public void InitMenu()
        {
            List<MenuModel> list = new List<MenuModel>();
            if (!_context.IsExist<MenuModel>(t => t.MenuName.Equals("系统管理菜单")))
            {
                ///icon-yunyinggongzuotai
                MenuModel menu = new MenuModel
                {
                    MenuName = "系统管理菜单",
                    MenuIcon = "layui-icon-set",
                    MenuIndex = 1,
                    MenuIsShow = 1,
                    MenuUrl = "",
                    MenuParentId = Guid.Empty
                };
                _context.Insert<MenuModel>(menu);
                MenuModel root = _context.GetEntity<MenuModel>(t => t.MenuParentId.Equals(Guid.Empty));

                menu = new MenuModel
                {
                    MenuName = "用户管理",
                    MenuIcon = "layui-icon-user",
                    MenuIndex = 1,
                    MenuIsShow = 1,
                    MenuUrl = "/System/UserIndex",
                    MenuParentId = root.Fid
                };
                list.Add(menu);
                menu = new MenuModel
                {
                    MenuName = "角色管理",
                    MenuIcon = "layui-icon-role",
                    MenuIndex = 2,
                    MenuIsShow = 1,
                    MenuUrl = "/System/RoleIndex",
                    MenuParentId = root.Fid
                };
                list.Add(menu);
                menu = new MenuModel
                {
                    MenuName = "菜单管理",
                    MenuIcon = "layui-icon-menu",
                    MenuIndex = 3,
                    MenuIsShow = 1,
                    MenuUrl = "/System/MenuIndex",
                    MenuParentId = root.Fid
                };
                list.Add(menu);
                menu = new MenuModel
                {
                    MenuName = "权限管理",
                    MenuIcon = "layui-icon-right",
                    MenuIndex = 4,
                    MenuIsShow = 1,
                    MenuUrl = "/System/RightIndex",
                    MenuParentId = root.Fid
                };
                list.Add(menu);
                menu = new MenuModel
                {
                    MenuName = "下拉管理",
                    MenuIcon = "layui-icon-right",
                    MenuIndex = 4,
                    MenuIsShow = 1,
                    MenuUrl = "/System/DropDown",
                    MenuParentId = root.Fid
                };
                list.Add(menu);
            }
            _context.Insert(list);
        }

        public void InitMenuFunction()
        {
            var menus = _context.GetLists<MenuModel>(t=>!t.MenuParentId.Equals(Guid.Empty));
            var role = _context.GetEntity<RoleModel>(t => t.RoleCode.Equals("SuperAdmin"));
            List<RoleMenuModel> rmList = new List<RoleMenuModel>();
            //添加角色菜单
            foreach (MenuModel menu in menus)
            {
                rmList.Add(new RoleMenuModel
                {
                    Fid = Guid.NewGuid(),
                    RoleId = role.Fid,
                    MenuId = menu.Fid,
                    CreatePerson = "NicholasLeo",
                    CreateTime = DateTime.Now
                });
            }
            //_context.Insert(rmList);
            //添加角色菜单功能
            var list = _context.GetLists<FunctionModel>(t=>t.FunctionName.Equals("编辑") || t.FunctionName.Equals("删除") || t.FunctionName.Equals("新增"));
            //通过角色获取菜单
            var rmFid = from m in _context.Set<MenuModel>().AsEnumerable()
                        join rm in _context.Set<RoleMenuModel>().AsEnumerable() on m.Fid equals rm.MenuId
                        join r in _context.Set<RoleModel>() on rm.RoleId equals r.Fid
                        where r.RoleName.Equals("系统超级管理员") && m.MenuName.Equals("用户管理")
                        select new
                        {
                            MenuId = rm.Fid
                        };
            //var rmFid = from m in _context.Set<MenuModel>().AsEnumerable()
            //            join rm in _context.Set<RoleMenuModel>().AsEnumerable() on m.Fid equals rm.MenuId
            //            join r in _context.Set<RoleModel>() on rm.RoleId equals r.Fid
            //            where r.RoleName.Equals("系统超级管理员") && m.MenuName.Equals("菜单管理")
            //            select new
            //            {
            //                MenuId = rm.Fid
            //            };
            List <RoleMenuFunctionModel> right = new List<RoleMenuFunctionModel>();
            Guid tempId = Guid.NewGuid();
            foreach (var i in rmFid)
            {
                tempId = i.MenuId;
            }
            foreach (FunctionModel item in list)
            {
                RoleMenuFunctionModel m = new RoleMenuFunctionModel();
                m.Fid = Guid.NewGuid();
                m.FunctionId = item.Fid;
                m.RoleMenuId = tempId;
                m.CreatePerson = "NicholasLeo";
                m.CreateTime = DateTime.Now;
                right.Add(m);
            }
            _context.Insert<RoleMenuFunctionModel>(right);
        }

        public void InitRole()
        {
            RoleModel role = new RoleModel();
            role.RoleName = "系统超级管理员";
            role.CreatePerson = "NicholasLeo";
            role.CreateTime = DateTime.Now;
            role.RoleCode = "SuperAdmin";
            role.Description = "系统的超级权限拥有系统的所有权限";
            if (!_context.IsExist<RoleModel>(t => t.RoleCode == "SuperAdmin"))
                _context.Insert<RoleModel>(role);
            new RoleModel();
            role.RoleName = "超级管理员";
            role.CreatePerson = "NicholasLeo";
            role.CreateTime = DateTime.Now;
            role.RoleCode = "Admin";
            role.Description = "系统的管理员，权限小于超级管理员";
            if (!_context.IsExist<RoleModel>(t => t.RoleCode == "Admin"))
                _context.Insert<RoleModel>(role);
        }

        public void InitUserRole()
        {
            UserModel user = _context.GetEntity<UserModel>(t => t.UserCode.ToLower().Equals("admin"));
            RoleModel role = _context.GetEntity<RoleModel>(t => t.RoleCode.ToLower().Equals("superadmin"));
            UserRoleModel userRole = new UserRoleModel();
            userRole.UserId = user.Fid;
            userRole.RoleId = role.Fid;
            userRole.CreatePerson = "NicholasLeo";
            userRole.CreateTime = DateTime.Now;
            if(!_context.IsExist<UserRoleModel>(t=>t.UserId.Equals(user.Fid) && t.RoleId.Equals(role.Fid)))
                _context.Insert<UserRoleModel>(userRole);
        }

        public void InitDropDown()
        {
            DropDownOptionsModel model = new DropDownOptionsModel();
            model.ParentId = Guid.Empty;
            model.MyName = "地区设置";
            model.MyValue = Guid.NewGuid();
            model.OptionsCode = "AreaSetting";
            model.Level = 0;
            model.CreatePerson = "NicholasLeo";
            model.CreateTime = DateTime.Now;
            if (!_context.IsExist<DropDownOptionsModel>(t => t.OptionsCode.Equals("AreaSetting")))
                _context.Insert<DropDownOptionsModel>(model);

            List<ChinaEnt> list = NLFrameJson.FileToObject<ChinaEnt>(AppDomain.CurrentDomain.BaseDirectory + "Configs\\China.json");
            if (list != null)
            {
                model = _context.GetEntity<DropDownOptionsModel>(t => t.OptionsCode.Equals("AreaSetting"));
                //List<DropDownOptionsModel> listData = GetChinaList(list, model.Fid);
                //if(!_context.IsExist<DropDownOptionsModel>(t=>t.ParentId.Equals(model.Fid)))
                //    _context.Insert<DropDownOptionsModel>(listData);
                CreateChinaList(list, model.Fid);
            }
        }

        private void CreateChinaList(List<ChinaEnt> list, Guid fid)
        {
            DropDownOptionsModel model = new DropDownOptionsModel();
            DropDownOptionsModel tempEnt = new DropDownOptionsModel();
            model.CreatePerson = "NicholasLeo";
            model.CreateTime = DateTime.Now;
            model.ParentId = fid;
            model.MyName = "中华人民共和国";
            model.MyValue = Guid.NewGuid();
            model.OptionsCode = "China";
            if(!_context.IsExist<DropDownOptionsModel>(t=>t.OptionsCode.Equals(model.OptionsCode)))
                _context.Insert<DropDownOptionsModel>(model);
            Guid gjid = _context.GetEntity<DropDownOptionsModel>(t => t.OptionsCode.Equals(model.OptionsCode)).Fid;
            foreach (ChinaEnt ent in list)
            {
                try
                {
                    //省
                    model = new DropDownOptionsModel();
                    model.CreatePerson = "NicholasLeo";
                    model.CreateTime = DateTime.Now;
                    model.MyName = ent.Name;
                    model.ParentId = gjid;
                    model.MyValue = Guid.NewGuid();
                    model.OptionsCode = ent.Code;
                    if (!_context.IsExist<DropDownOptionsModel>(t => t.OptionsCode.Equals(model.OptionsCode)))
                        _context.Insert<DropDownOptionsModel>(model);
                    if (ent.City == null || ent.City.Count == 0)
                        continue;
                    List<CityEnt> cities = ent.City;
                    Guid sfid = _context.GetEntity<DropDownOptionsModel>(t => t.OptionsCode.Equals(model.OptionsCode)).Fid;
                    foreach (CityEnt city in cities)
                    {
                        try
                        {
                            //市
                            model = new DropDownOptionsModel();
                            model.CreatePerson = "NicholasLeo";
                            model.CreateTime = DateTime.Now;
                            model.ParentId = sfid;
                            model.MyName = city.Name;
                            model.MyValue = Guid.NewGuid();
                            model.OptionsCode = city.Code;
                            if (!_context.IsExist<DropDownOptionsModel>(t => t.OptionsCode.Equals(model.OptionsCode)))
                                _context.Insert<DropDownOptionsModel>(model);
                            if (city.Area == null || city.Area.Count == 0)
                                continue;
                            List<ChinaBaseEnt> areas = city.Area;
                            Guid csid = _context.GetEntity<DropDownOptionsModel>(t => t.OptionsCode.Equals(model.OptionsCode)).Fid;
                            foreach (ChinaBaseEnt item in areas)
                            {
                                try
                                {
                                    //县
                                    model = new DropDownOptionsModel();
                                    model.CreatePerson = "NicholasLeo";
                                    model.CreateTime = DateTime.Now;
                                    model.ParentId = csid;
                                    model.MyName = item.Name;
                                    model.MyValue = Guid.NewGuid();
                                    model.OptionsCode = item.Code;
                                    if (!_context.IsExist<DropDownOptionsModel>(t => t.OptionsCode.Equals(model.OptionsCode)))
                                        _context.Insert<DropDownOptionsModel>(model);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(ex.Message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
