//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 14:41:10
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model
{
    
    public class NvaMenus : MenuModel
    {
        public List<MenuModel> ChildMenus { get; set; }
    }
}
