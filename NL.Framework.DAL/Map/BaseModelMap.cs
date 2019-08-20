//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-12 14:38:31
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.DAL.Map
{
    public abstract class BaseModelMap<T> : EntityTypeConfiguration<T> where T : BaseModel
    {
        public BaseModelMap()
        {
            HasKey(t => t.Fid);

            Property(t => t.Fid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CreateTime).HasColumnType(SqlDataType.DATETIME2);

            Property(t => t.CreatePerson).HasColumnType(SqlDataType.CSTRING).HasMaxLength(30);

            Property(t => t.ModifyPerson).HasColumnType(SqlDataType.CSTRING).HasMaxLength(30);

            Property(t => t.ModifyTime).HasColumnType(SqlDataType.DATETIME2);
        }
    }
}
