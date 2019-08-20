//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-20 1:09:33
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model;
using NL.Framework.Model.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.DAL.Map.Order
{
    public abstract class OrderBaseModelMap<T> : EntityTypeConfiguration<T> where T : OrderBaseModel
    {
        public OrderBaseModelMap() : base()
        {
            HasKey(t => t.Fid);

            Property(t => t.Fid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CreateTime).HasColumnType(SqlDataType.DATETIME2);

            Property(t => t.CreatePerson).HasColumnType(SqlDataType.CSTRING).HasMaxLength(30);

            Property(t => t.ModifyPerson).HasColumnType(SqlDataType.CSTRING).HasMaxLength(30);

            Property(t => t.ModifyTime).HasColumnType(SqlDataType.DATETIME2);

            Property(t => t.Ext1).HasColumnType(SqlDataType.CSTRING).HasMaxLength(100);
            Property(t => t.Ext2).HasColumnType(SqlDataType.CSTRING).HasMaxLength(100);
            Property(t => t.Ext3).HasColumnType(SqlDataType.CSTRING).HasMaxLength(100);
            Property(t => t.Ext4).HasColumnType(SqlDataType.CSTRING).HasMaxLength(100);
            Property(t => t.Ext5).HasColumnType(SqlDataType.CSTRING).HasMaxLength(100);
            Property(t => t.Ext6).HasColumnType(SqlDataType.CSTRING).HasMaxLength(100);
            Property(t => t.Ext7).HasColumnType(SqlDataType.CSTRING).HasMaxLength(100);
            Property(t => t.Ext8).HasColumnType(SqlDataType.CSTRING).HasMaxLength(100);
            Property(t => t.Ext9).HasColumnType(SqlDataType.CSTRING).HasMaxLength(100);
            Property(t => t.Ext10).HasColumnType(SqlDataType.CSTRING).HasMaxLength(100);
        }
    }
}
