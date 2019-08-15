//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-15 15:37:58
//    说明：
//    版权所有：个人
//***********************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model
{
    public class ChinaEnt : ChinaBaseEnt
    {
        [JsonProperty(PropertyName = "city")]
        public List<CityEnt> City { get; set; }
    }

    public class CityEnt : ChinaBaseEnt
    {
        [JsonProperty(PropertyName = "area")]
        public List<ChinaBaseEnt> Area { get; set; }
    }

    public class ChinaBaseEnt
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
    }
}
