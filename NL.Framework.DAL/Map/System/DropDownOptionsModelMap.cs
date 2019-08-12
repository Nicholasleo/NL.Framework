//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-12 14:35:04
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.DAL.Map.System
{
    public class DropDownOptionsModelMap : BaseModelMap<DropDownOptionsModel>
    {
        public DropDownOptionsModelMap() : base()
        {
            ToTable(TableName._DROPDOWN);
            Property(t => t.Fid).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
