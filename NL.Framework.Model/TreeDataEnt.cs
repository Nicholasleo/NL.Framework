//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-06 14:00:10
//    说明：
//    版权所有：个人
//***********************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model
{
    public class TreeDataEnt
    {
        public TreeBaseEnt TreeData { get; set; }
    }

    public class TreeBaseEnt
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Name { get; set; }

        [DefaultValue(false)]
        [JsonProperty(PropertyName = "disabled")]
        public bool Disabled { get; set; }

        [DefaultValue(false)]
        [JsonProperty(PropertyName= "checked")]
        public bool IsChecked{get;set; }

        [JsonProperty(PropertyName = "children")]
        public List<TreeBaseEnt> Childrens { get; set; }
    }
}
