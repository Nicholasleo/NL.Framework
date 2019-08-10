//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-10 18:23:15
//    说明：
//    版权所有：个人
//***********************************************************
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NL.Framework.Common.Log
{
    public class Logger : ILogger
    {
        private static ILog _Log;

        static Logger()
        {
            FileInfo configFile = new FileInfo(HttpContext.Current.Server.MapPath("/Configs/log4net.config"));
            log4net.Config.XmlConfigurator.Configure(configFile);
            _Log = LogManager.GetLogger(typeof(Logger));
        }

        public void Debug(string value)
        {
            if (_Log.IsDebugEnabled)
            {
                _Log.Debug(value);
            }
        }

        public void Debug(string value, Exception ex)
        {
            if (_Log.IsDebugEnabled)
            {
                _Log.Debug(value,ex);
            }
        }

        public void Error(string value)
        {
            if (_Log.IsErrorEnabled)
            {
                _Log.Error(value);
            }
        }

        public void Error(string value, Exception ex)
        {
            if (_Log.IsErrorEnabled)
            {
                _Log.Error(value,ex);
            }
        }

        public void Info(string value)
        {
            if (_Log.IsInfoEnabled)
            {
                _Log.Info(value);
            }
        }
        public void Info(string value, Exception ex)
        {
            if (_Log.IsInfoEnabled)
            {
                _Log.Info(value, ex);
            }
        }

        public void Warn(string value)
        {
            if (_Log.IsWarnEnabled)
            {
                _Log.Warn(value);
            }
        }

        public void Warn(string value, Exception ex)
        {
            if (_Log.IsWarnEnabled)
            {
                _Log.Warn(value,ex);
            }
        }
    }
}
