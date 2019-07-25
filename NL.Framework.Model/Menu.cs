//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 14:41:10
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
    public class Menu
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
    }

    public class TopMenu : Menu
    {
        public List<Menu> Childs { get; set; }
    }

    public class NvaMenu : Menu
    {
        public bool HasChild { get; set; }
        public List<Menu> Childs { get; set; }
    }
}
