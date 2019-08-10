﻿//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:04:32
//    说明：
//    版权所有：个人
//***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.IBLL
{
    public interface ISystemInit : IBaseBll
    {
        void InitMenu();
        void InitRole();
        void InitFunction();
        void InitUser();
        void InitMenuFunction();
        void InitUserRole();
    }
}
