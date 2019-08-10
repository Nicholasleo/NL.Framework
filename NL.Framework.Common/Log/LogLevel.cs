//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-10 17:49:02
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
    /// <summary>日志等级</summary>
    public enum LogLevel : System.Byte
    {
        /// <summary>打开所有日志记录</summary>
        ALL = 0,
        /// <summary>最低调试。细粒度信息事件对调试应用程序非常有帮助</summary>
        DEBUG,
        /// <summary>普通消息。在粗粒度级别上突出强调应用程序的运行过程</summary>
        INFO,
        /// <summary>警告</summary>
        WARN,
        /// <summary>错误</summary>
        ERROR,
        /// <summary>严重错误</summary>
        FATAL,
        /// <summary>关闭所有日志记录</summary>
        OFF = 0xFF
    }
}
