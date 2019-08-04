//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 18:22:20
//    说明：
//    版权所有：个人
//***********************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model.System
{
    /// <summary>
    /// 系统菜单实体模型
    /// </summary>
    public partial class MenuModel : SystemBaseModel
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get;set; }
        /// <summary>
        /// 菜单链接地址
        /// </summary>
        public string MenuUrl { get; set; }
        /// <summary>
        /// 菜单显示图标
        /// </summary>
        public string MenuIcon { get; set; }
        /// <summary>
        /// 菜单父级ID
        /// </summary>
        public Guid MenuParentId { get; set; }
        /// <summary>
        /// 是否显示菜单
        /// </summary>
        public int MenuIsShow { get; set; }
        /// <summary>
        /// 菜单显示顺序
        /// </summary>
        public int MenuIndex { get; set; }
        /// <summary>
        /// 角色关联菜单
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<RoleMenuModel> RoleMenuModels { get; set; }
    }
}
