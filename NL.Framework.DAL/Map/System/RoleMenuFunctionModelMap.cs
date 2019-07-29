//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 18:34:11
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model.System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NL.Framework.DAL.Map
{
    public class RoleMenuFunctionModelMap : EntityTypeConfiguration<RoleMenuFunctionModel>
    {
        public RoleMenuFunctionModelMap()
        {
            ToTable(TableName._ROLEMENUFUNCTION);

            HasKey(t => t.Fid);

            //HasIndex(t => t.FunctionId).IsUnique();
            //HasIndex(t => t.MenuId).IsUnique();
            //HasIndex(t => t.RoleId).IsUnique();

            Property(t => t.Fid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CreateTime).HasColumnType("DATETIME2");

            Property(t => t.CreatePerson).HasColumnType("NVARCHAR").HasMaxLength(30);

            Property(t => t.ModifyPerson).HasColumnType("NVARCHAR").HasMaxLength(30);

            Property(t => t.ModifyTime).HasColumnType("DATETIME2");

            //HasMany(t => t.RoleModels).WithMany(t => t.MenuModels).Map(t => t.ToTable(TableName._ROLEMENU).MapLeftKey("MenuId").MapRightKey("RoleId"));

            //HasMany(t => t.FunctionModels).WithMany(t => t.MenuModels).Map(t => t.ToTable(TableName._ROLEMENUFUNCTION).MapLeftKey("MenuId").MapRightKey("FunctionId"));

        }
    }
}
