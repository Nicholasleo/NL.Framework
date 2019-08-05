//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-04 10:44:15
//    说明：
//    版权所有：个人
//***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model.System
{
    public class DropDownOptionsModel : SystemBaseModel
    {
        /// <summary>
        /// 所属类型GUID
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string MyName { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public int MyValue { get; set; }
    }
}
