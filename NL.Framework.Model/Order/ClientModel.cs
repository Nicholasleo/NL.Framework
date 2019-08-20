//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-19 23:23:38
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
    /// 门店实体类
    /// </summary>
    public class ClientModel : OrderBaseModel
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        public string ClientCode { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 门店等级
        /// </summary>
        public int ClientLevel { get; set; }
        /// <summary>
        /// 门店类型
        /// </summary>
        public string ClientType { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        public string Corporation { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }
        /// <summary>
        /// 门店地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 门店电话
        /// </summary>
        public string ClientTel { get; set; }
        /// <summary>
        /// 门店地区
        /// </summary>
        public string ClientArea { get; set; }
        /// <summary>
        /// 门店大区
        /// </summary>
        public string ClientRegion { get; set; }
        /// <summary>
        /// 订货模式(散码|配码)
        /// </summary>
        public bool OrderModel { get; set; }
        /// <summary>
        /// 门店折扣率
        /// </summary>
        public decimal ClientRate { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrganClientModel> OrganClientModels { get; set; }
    }
}
