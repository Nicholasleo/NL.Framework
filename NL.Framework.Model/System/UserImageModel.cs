//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-15 12:24:05
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
    public class UserImageModel : SystemBaseModel
    {
        public byte[] UserIcon { get; set; }

        [JsonIgnore]
        public virtual UserModel UserModel { get; set; }
    }
}
