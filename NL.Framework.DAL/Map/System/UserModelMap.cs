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
    public class UserModelMap : BaseModelMap<UserModel>
    {
        public UserModelMap() : base()
        {
            ToTable(TableName._USER);

            Property(t => t.Fid).HasColumnName("UserId");

            Property(t => t.UserName).HasColumnType("NVARCHAR").HasMaxLength(50);

            Property(t => t.UserCode).IsRequired().HasColumnType("NVARCHAR").HasMaxLength(20);
            //添加唯一约束
            HasIndex(t => t.UserCode).IsUnique();

            Property(t => t.IdCard).IsRequired().HasColumnType("NVARCHAR").HasMaxLength(18);
            HasIndex(t => t.IdCard).IsUnique();

            Property(t => t.UserPwd).IsRequired().HasColumnType("NVARCHAR").HasMaxLength(24);

            Property(t => t.Gender);
            Property(t => t.UserAge);
            Property(t => t.Email).HasColumnType("NVARCHAR").HasMaxLength(30);
            Property(t => t.WeChat).HasColumnType("NVARCHAR").HasMaxLength(30);
            Property(t => t.QQ).HasColumnType("NVARCHAR").HasMaxLength(20);
            Property(t => t.MobilePhone).HasColumnType("NVARCHAR").HasMaxLength(12);
            Property(t => t.Address).HasColumnType("NVARCHAR").HasMaxLength(300);
            Property(t => t.IsDelete);
            Property(t => t.IsAdmin);
            Property(t => t.FirstLoginTime).HasColumnType("DATETIME2");
            Property(t => t.LastLoginTime).HasColumnType("DATETIME2");
            Property(t => t.State);
            Property(t => t.Description).HasColumnType("NVARCHAR").HasMaxLength(300);
            HasMany(t => t.UserRoleModels).WithRequired(t => t.UserModel).HasForeignKey(t => t.UserId);

        }
    }
}
