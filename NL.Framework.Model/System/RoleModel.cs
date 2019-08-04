//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 18:22:45
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
    /// 角色实体模型
    /// </summary>
    public partial class RoleModel : SystemBaseModel
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色编号，非主键
        /// </summary>
        public string RoleCode { get; set; }
        //public Guid ParentFid { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 角色关联用户
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserRoleModel> UserRoleModels { get; set; }
        /// <summary>
        /// 角色关联菜单
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<RoleMenuModel> RoleMenuModels { get; set; }

    }
}
