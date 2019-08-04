//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 18:22:31
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
    public class FunctionModel : SystemBaseModel
    {
        public string FunctionName { get; set; }
        public string FunctionEvent { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoleMenuFunctionModel> RoleMenuFunctionModels { get; set; }
    }
}
