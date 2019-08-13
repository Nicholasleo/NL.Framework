//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-12 14:35:04
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model.System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NL.Framework.DAL.Map.System
{
    public class DropDownOptionsModelMap : BaseModelMap<DropDownOptionsModel>
    {
        public DropDownOptionsModelMap() : base()
        {
            ToTable(TableName._DROPDOWN);

            Property(t => t.ParentId);

            Property(t => t.OptionsCode).HasMaxLength(50).HasColumnType("NVARCHAR");

            //索引键，唯一存在
            HasIndex(t => t.OptionsCode).IsUnique();

            Property(t => t.MyName).HasColumnType("NVARCHAR").HasMaxLength(100);

            Property(t => t.MyValue);

            Property(t=>t.Level).HasColumnName("INT").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
