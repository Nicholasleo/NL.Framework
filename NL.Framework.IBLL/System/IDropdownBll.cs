﻿//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-12 17:53:52
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;

namespace NL.Framework.IBLL
{
    public interface IDropdownBll : ISystemBaseBll<DropDownOptionsModel>,IBaseBll
    {
        List<DropDownTreeEnt> GetTreeLists(Guid fid);
    }
}
