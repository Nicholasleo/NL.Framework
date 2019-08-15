//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 18:37:10
//    说明：
//    版权所有：个人
//***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.DAL
{
    public static class TableName
    {
        /// <summary>
        /// 系统菜单表
        /// </summary>
        public const string _MENU = "Sys_Menu";
        /// <summary>
        /// 系统用户表
        /// </summary>
        public const string _USER = "Sys_User";
        /// <summary>
        /// 系统角色表
        /// </summary>
        public const string _ROLE = "Sys_Role";
        /// <summary>
        /// 系统功能表
        /// </summary>
        public const string _FUNCTION = "Sys_Function";
        /// <summary>
        /// 系统下拉框配置表
        /// </summary>
        public const string _DROPDOWN = "Sys_Dropdown";

        /// <summary>
        /// 用户头像表
        /// </summary>
        public const string _USERIMAGE = "Sys_UserImage";




        /// <summary>
        /// 菜单-功能关系表
        /// </summary>
        public const string _MENUFUNCTION = "Sys_MenuFunction";
        /// <summary>
        /// 角色-菜单-功能关系表
        /// </summary>
        public const string _ROLEMENUFUNCTION = "Sys_RoleMenuFunction";
        /// <summary>
        /// 角色-菜单关系表
        /// </summary>
        public const string _ROLEMENU = "Sys_RoleMenu";
        /// <summary>
        /// 用户-角色关系表
        /// </summary>
        public const string _USERROLE = "Sys_UserRole";

        //public const string _MENUROLE = "Sys_Menu";
        //public const string _MENUROLE = "Sys_Menu";
        //public const string _MENUROLE = "Sys_Menu";
        //public const string _MENUROLE = "Sys_Menu";
        //public const string _MENUROLE = "Sys_Menu";
    }
}
