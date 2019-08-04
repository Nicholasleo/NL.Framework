using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-31 10:51:35 
//    说明：
//    版权所有：个人
//***********************************************************
namespace NL.Framework.Model.System
{
    public class RoleMenuModel : SystemBaseModel
    {
        public Guid RoleId { get; set; }
        public virtual RoleModel RoleModel { get; set; }
        public Guid MenuId { get; set; }
        public virtual MenuModel MenuModel { get; set; }

        [JsonIgnore]

        public virtual ICollection<RoleMenuFunctionModel> RoleMenuFunctionModels { get; set; }
    }
}
