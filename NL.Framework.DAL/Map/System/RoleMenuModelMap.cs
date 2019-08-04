using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-31 11:06:59 
//    说明：
//    版权所有：个人
//***********************************************************
namespace NL.Framework.DAL.Map.System
{
    public class RoleMenuModelMap : EntityTypeConfiguration<RoleMenuModel>
    {
        public RoleMenuModelMap()
        {
            ToTable(TableName._ROLEMENU);

            HasKey(t => t.Fid);

            Property(t => t.Fid).HasColumnName("RoleMenuId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CreateTime).HasColumnType("DATETIME2");

            Property(t => t.CreatePerson).HasColumnType("NVARCHAR").HasMaxLength(30);

            Property(t => t.ModifyPerson).HasColumnType("NVARCHAR").HasMaxLength(30);

            Property(t => t.ModifyTime).HasColumnType("DATETIME2");

            //HasOptional(t => t.RoleModel).WithOptionalPrincipal(l => l.RoleMenuModel).Map(t=>t.MapKey("RoleMenuId"));

            HasMany(t => t.RoleMenuFunctionModels).WithRequired(t => t.RoleMenuModel).HasForeignKey(t => t.RoleMenuId);
        }
    }
}
