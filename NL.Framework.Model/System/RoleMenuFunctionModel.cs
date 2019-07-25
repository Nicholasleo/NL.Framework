//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 18:23:57
//    说明：
//    版权所有：个人
//***********************************************************
using System;

namespace NL.Framework.Model.System
{
    public class RoleMenuFunctionModel : SystemBaseModel
    {
        public Guid RoleId { get; set; }
        public virtual RoleModel RoleModel { get; set; }
        public Guid MenuId { get; set; }
        public virtual MenuModel MenuModel { get; set; }
        public Guid FunctionId { get; set; }
        public virtual FunctionModel FunctionModel { get; set; }
    }
}
