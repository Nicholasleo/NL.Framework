//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-20 0:08:00
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
    public class ClientModelMap : OrderBaseModelMap<ClientModel>
    {
        public ClientModelMap() : base()
        {
            ToTable(TableName._CLIENT);

            Property(t => t.Fid).HasColumnName("ClientId");
            Property(t => t.ClientCode).HasColumnType(SqlDataType.STRING).HasMaxLength(50).IsRequired();
            Property(t => t.Address).HasColumnType(SqlDataType.CSTRING).HasMaxLength(300);
            Property(t => t.ClientArea).HasMaxLength(20).HasColumnType(SqlDataType.CSTRING);
            Property(t => t.ClientLevel).HasColumnType(SqlDataType.INT);
            Property(t => t.ClientName).HasColumnType(SqlDataType.CSTRING).HasMaxLength(50).IsRequired();
            Property(t => t.ClientRate).HasColumnType(SqlDataType.DECIMAL).HasPrecision(4,2);
            Property(t => t.ClientRegion).HasMaxLength(30).HasColumnType(SqlDataType.CSTRING);
            Property(t => t.ClientTel).HasColumnType(SqlDataType.STRING).HasMaxLength(15);
            Property(t => t.ClientType).HasMaxLength(20).HasColumnType(SqlDataType.CSTRING);
            Property(t => t.Contacts).HasColumnType(SqlDataType.CSTRING).HasMaxLength(10);
            Property(t => t.Corporation).HasMaxLength(10).HasColumnType(SqlDataType.CSTRING);
            Property(t => t.OrderModel).HasColumnName(SqlDataType.BOOLEAN);
            Property(t => t.State).HasColumnName(SqlDataType.INT).IsRequired();

            HasIndex(t => t.ClientCode).IsUnique();
            HasMany(t => t.OrganClientModels).WithRequired(t => t.ClientModel).HasForeignKey(t => t.ClientId);
        }
    }
}
