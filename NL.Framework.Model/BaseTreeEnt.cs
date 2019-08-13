//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-13 19:07:28
//    说明：
//    版权所有：个人
//***********************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NL.Framework.Model
{
    public class DropdownTreeEnt
    {
        [JsonProperty(PropertyName = "status")]
        public TreeDataStatusEnt DataStatus { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<BaseTreeEnt> TreeData { get; set; }
    }

    public class BaseTreeEnt
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }


        [JsonProperty(PropertyName = "title")]
        public string Name { get; set; }


        [JsonProperty(PropertyName = "parentId")]
        public Guid ParentId { get; set; }

        [JsonProperty(PropertyName = "children")]
        public List<RightTreeBaseEnt> Childrens { get; set; }
    }
}
