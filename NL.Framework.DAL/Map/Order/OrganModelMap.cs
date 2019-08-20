//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-20 0:54:39
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
    public class OrganModelMap : OrderBaseModelMap<OrganModel>
    {
        public OrganModelMap() : base()
        {
            ToTable(TableName._ORGAN);

            Property(t => t.Fid).HasColumnName("OrganId");
            Property(t => t.ParentId);
            Property(t => t.OrderModel).HasColumnType(SqlDataType.BOOLEAN);
            Property(t => t.OrganArea).HasMaxLength(30).HasColumnType(SqlDataType.CSTRING);
            Property(t => t.OrganCode).HasColumnType(SqlDataType.STRING).HasMaxLength(30).IsRequired();
            Property(t => t.OrganLevel).HasColumnType(SqlDataType.INT);
            Property(t => t.OrganName).HasColumnType(SqlDataType.CSTRING).HasMaxLength(50).IsRequired();
            Property(t => t.OrganRate).HasColumnType(SqlDataType.DECIMAL).HasPrecision(4, 2);
            Property(t => t.OrganRegion).HasMaxLength(30).HasColumnType(SqlDataType.CSTRING);
            Property(t => t.OrganType).HasColumnType(SqlDataType.CSTRING).HasMaxLength(10);
            Property(t => t.SelectStyleType).HasColumnType(SqlDataType.INT);
            Property(t => t.State).HasColumnType(SqlDataType.INT);

            HasIndex(t => t.OrganCode).IsUnique();
            HasMany(t => t.OrganClientModels).WithRequired(t => t.OrganModel).HasForeignKey(t => t.OrgandId);
        }
    }
}
