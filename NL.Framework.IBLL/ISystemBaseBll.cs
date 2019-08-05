//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:52:35
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;

namespace NL.Framework.IBLL
{
    public interface ISystemBaseBll
    {
        List<FunctionModel> GetMenuFunction();
        List<FunctionModel> GetMenuFunction(Guid menuFid,Guid roleFid);
        List<FunctionModel> GetMenuFunction(Guid menuFid, string roleCode);
        List<FunctionModel> GetMenuFunction(string menuName, string roleCode);
        List<FunctionModel> GetMenuFunction(string menuName, Guid roleFid);
    }
}
