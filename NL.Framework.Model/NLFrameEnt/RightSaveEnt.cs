//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-07 2:35:24
//    说明：
//    版权所有：个人
//***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model
{
    public class RightSaveEnt
    {
        public Guid RoleId { get; set; }
        public List<RoleMenuEnt> RoleMenuEnts { get; set; }
        public List<RoleMenuFunctionEnt> RoleMenuFunctionEnts { get; set; }
    }

    public class RoleMenuEnt
    {
        public Guid MenuId { get; set; }
    }

    public class RoleMenuFunctionEnt : RoleMenuEnt
    {
        public Guid FunctionId { get; set; }
    }
}
