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
        [JsonProperty(PropertyName = "status")]
        public TreeDataStatusEnt DataStatus { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<RightTreeBaseEnt> TreeData { get; set; }
    }

    public class TreeDataStatusEnt
    {
        [JsonProperty(PropertyName = "code")]
        [DefaultValue("404")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        [DefaultValue("获取数据失败！")]
        public string Message { get; set; }
    }

    public class RightTreeBaseEnt : BaseTreeEnt
    {
        private bool _spread = true;
        public RightTreeBaseEnt()
        {
            this.Spread = _spread;
        }

        [JsonProperty(PropertyName = "hide")]
        [DefaultValue(false)]
        public bool Hide { get; set; }

        /** 自定义图标class*/
        [JsonProperty(PropertyName = "iconClass")]
        [DefaultValue("")]
        public string IconClass { get; set; }

        [JsonProperty(PropertyName = "checkArr")]
        public List<CheckArr> CheckArrs { get; set; }

        [JsonProperty(PropertyName = "children")]
        public List<RightTreeBaseEnt> Childrens { get; set; }
    }

    public class CheckArr
    {
        private string _treeType = "0";
        private string _isChecked = "0";
        public CheckArr()
        {
            this.IsChecked = _isChecked;
            this.TreeType = _treeType;
        }

        public CheckArr(string value,string type = "0")
        {
            this.TreeType = type;
            this.IsChecked = value;
        }

        [JsonProperty(PropertyName = "type")]
        [DefaultValue("0")]
        public string TreeType { get; set; }

        [JsonProperty(PropertyName = "checked")]
        [DefaultValue("0")]
        public string IsChecked { get; set; }
    }
}
