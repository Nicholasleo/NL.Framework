//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:05:40
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.IBLL;
using NL.Framework.IDAL;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _context.Insert(list);
        }

        public void InitMenu()
        {
            List<MenuModel> list = new List<MenuModel>();
            if (!_context.IsExist<MenuModel>(t => t.MenuName.Equals("系统管理菜单")))
            {
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
            }
            _context.Insert(list);
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
    }
}
