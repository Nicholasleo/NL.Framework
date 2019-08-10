//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-10 17:26:12
//    说明：
//    版权所有：个人
//***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Common.Log
{
    public interface ILogger
    {
        void Debug(string value);

        void Debug(string value, Exception ex);

        void Info(string value);
        void Info(string value, Exception ex);

        void Error(string value);

        void Error(string value, Exception ex);

        void Warn(string value);
        void Warn(string value, Exception ex);
    }
}
