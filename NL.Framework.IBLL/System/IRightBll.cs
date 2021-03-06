﻿//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-06 15:09:23
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model;
using System;
using System.Collections.Generic;

namespace NL.Framework.IBLL
{
    public interface IRightBll : IBaseBll
    {
        List<RightTreeBaseEnt> GetTreeLists(Guid roleFid);

        AjaxResultEnt SaveRoleRight(RightSaveEnt data);
    }
}
