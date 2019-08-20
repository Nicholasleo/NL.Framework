//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-19 23:28:07
//    说明：
//    版权所有：个人
//***********************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model.Order
{
    /// <summary>
    /// 机构实体类型
    /// </summary>
    public class OrganModel : OrderBaseModel
    {
        /// <summary>
        /// 机构上级ID
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 机构编号
        /// </summary>
        public string OrganCode { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrganName { get; set; }
        /// <summary>
        /// 机构等级
        /// </summary>
        public int OrganLevel { get; set; }
        /// <summary>
        /// 机构类型
        /// </summary>
        public string OrganType { get; set; }
        /// <summary>
        /// 机构大区
        /// </summary>
        public string OrganRegion { get; set; }
        /// <summary>
        /// 机构地区
        /// </summary>
        public string OrganArea { get; set; }
        /// <summary>
        /// 机构订货模式(散码|配码)
        /// </summary>
        public bool OrderModel { get; set; }
        /// <summary>
        /// 圈款模式(1：不启用圈款,2：启用圈款,3：有圈款则优先圈款)
        /// </summary>
        public int SelectStyleType { get; set; }
        /// <summary>
        /// 机构折扣率
        /// </summary>
        public decimal OrganRate { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrganClientModel> OrganClientModels { get; set; }
    }
}
