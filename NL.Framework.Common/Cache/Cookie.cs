//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-07 14:24:29
//    说明：
//    版权所有：个人
//***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NL.Framework.Common.Cache
{
    public static class Session
    {
        public static void SetCookie(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static T GetSession<T>(string key)
        {
            try
            {
                var Obj = (T)(HttpContext.Current.Session[key]);
                if (Obj == null)
                    return (T)Activator.CreateInstance(typeof(T));
                return Obj;
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
