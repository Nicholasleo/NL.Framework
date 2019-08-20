//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-19 23:58:45
//    说明：
//    版权所有：个人
//***********************************************************
using System;

namespace NL.Framework.Model.Order
{
    public class OrganClientModel : BaseModel
    {
        public Guid ClientId { get; set; }
        public virtual ClientModel ClientModel { get; set; }
        public Guid OrgandId { get; set; }
        public virtual OrganModel OrganModel { get; set; }
    }
}
