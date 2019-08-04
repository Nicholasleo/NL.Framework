//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 18:23:57
//    说明：
//    版权所有：个人
//***********************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NL.Framework.Model.System
{
    public class RoleMenuFunctionModel : SystemBaseModel
    {
        public Guid RoleMenuId { get; set; }

        [JsonIgnore]
        public virtual RoleMenuModel RoleMenuModel { get; set; }

        public Guid FunctionId { get; set; }

        [JsonIgnore]
        public virtual FunctionModel FunctionModel { get; set; }
    }
}
