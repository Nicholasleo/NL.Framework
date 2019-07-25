//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 18:23:26
//    说明：
//    版权所有：个人
//***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model.System
{
    public class UserRoleModel : SystemBaseModel
    {
        public Guid UserId { get; set; }
        public virtual UserModel UserModel { get; set; }
        public Guid RoleId { get; set; }
        public virtual RoleModel RoleModel { get; set; }
    }
}
