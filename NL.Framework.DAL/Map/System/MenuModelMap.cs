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
    public class MenuModelMap : EntityTypeConfiguration<MenuModel>
    {
        public MenuModelMap()
        {
            ToTable(TableName._MENU);

            HasKey(t => t.Fid);

            Property(t => t.Fid).HasColumnName("MenuId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.MenuName).HasColumnType("NVARCHAR").HasMaxLength(50);

            Property(t => t.MenuUrl).HasColumnType("NVARCHAR").HasMaxLength(200);

            Property(t => t.MenuIcon).HasColumnType("NVARCHAR").HasMaxLength(100);

            Property(t => t.MenuParentId);

            Property(t => t.MenuIcon).HasColumnType("NVARCHAR").HasMaxLength(50);

            Property(t => t.MenuIsShow);

            Property(t => t.MenuIndex);

            Property(t => t.CreateTime).HasColumnType("DATETIME2");

            Property(t => t.CreatePerson).HasColumnType("NVARCHAR").HasMaxLength(30);

            Property(t => t.ModifyPerson).HasColumnType("NVARCHAR").HasMaxLength(30);

            Property(t => t.ModifyTime).HasColumnType("DATETIME2");

            HasMany(t => t.RoleMenuModels).WithRequired(t => t.MenuModel).HasForeignKey(t => t.MenuId);
            //HasOptional(t => t.RoleMenuModel).WithOptionalDependent(l => l.MenuModel).Map(t=>t.MapKey("MenuId"));

        }
    }
}
