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
    public class UserRoleModelMap : EntityTypeConfiguration<UserRoleModel>
    {
        public UserRoleModelMap()
        {
            ToTable(TableName._USERROLE);

            HasKey(t => t.Fid);

            Property(t => t.Fid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //HasIndex(t => t.UserId).IsUnique();

            //HasIndex(t => t.RoleId).IsUnique();

            Property(t => t.UserId);

            Property(t => t.RoleId);

            Property(t => t.CreateTime).HasColumnType("DATETIME2");

            Property(t => t.CreatePerson).HasColumnType("NVARCHAR").HasMaxLength(30);

            Property(t => t.ModifyPerson).HasColumnType("NVARCHAR").HasMaxLength(30);

            Property(t => t.ModifyTime).HasColumnType("DATETIME2");

        }
    }
}
