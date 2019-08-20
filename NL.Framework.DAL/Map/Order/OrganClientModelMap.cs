//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-20 1:13:23
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.DAL.Map.Order
{
    public class OrganClientModelMap : BaseModelMap<OrganClientModel>
    {
        public OrganClientModelMap()
        {
            ToTable(TableName._ORGANCLIENT);

            Property(t => t.ClientId);

            Property(t => t.OrgandId);
        }
    }
}
