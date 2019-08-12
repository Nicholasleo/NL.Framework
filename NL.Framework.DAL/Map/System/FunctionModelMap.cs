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
    public class FunctionModelMap : BaseModelMap<FunctionModel>
    {
        public FunctionModelMap() : base()
        {
            ToTable(TableName._FUNCTION);

            Property(t => t.Fid).HasColumnName("FunctionId");

            Property(t => t.FunctionName).HasColumnType("NVARCHAR").HasMaxLength(50);

            HasMany(t => t.RoleMenuFunctionModels).WithRequired(t => t.FunctionModel).HasForeignKey(t => t.FunctionId);

        }
    }
}
