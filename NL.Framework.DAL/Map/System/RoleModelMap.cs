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
    public class RoleModelMap : EntityTypeConfiguration<RoleModel>
    {
        public RoleModelMap()
        {
            ToTable(TableName._ROLE);

            HasKey(t => t.Fid);

            Property(t => t.Fid).HasColumnName("RoleId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.RoleName).HasColumnType("NVARCHAR").HasMaxLength(50);

            Property(t => t.RoleCode).HasColumnType("NVARCHAR").HasMaxLength(200);

            Property(t => t.Description).HasColumnType("NVARCHAR").HasMaxLength(200);

            Property(t => t.CreateTime).HasColumnType("DATETIME2");

            Property(t => t.CreatePerson).HasColumnType("NVARCHAR").HasMaxLength(30);

            Property(t => t.ModifyPerson).HasColumnType("NVARCHAR").HasMaxLength(30);

            Property(t => t.ModifyTime).HasColumnType("DATETIME2");

            HasMany(t => t.UserRoleModels).WithRequired(t => t.RoleModel).HasForeignKey(t => t.RoleId);

            HasMany(t => t.RoleMenuFunctionModels).WithRequired(t => t.RoleModel).HasForeignKey(t => t.RoleId);

        }
    }
}
