//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-10 11:07:31
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Common.Config;
using NL.Framework.Common.Security;
using System.Configuration;
using System.Web;

namespace NL.Framework.Common
{
    public sealed class Licence
    {
        public static bool IsLicence(string key)
        {
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            if (host.Equals("localhost"))
                return true;
            string licence = ConfigurationManager.AppSettings[SystemParameters.NLFRAME_SYSTEM_LICENCE];
            if (licence != null && licence == Md5.MD5Encrypt(key, 32))
                return true;

            return false;
        }
        public static string GetLicence()
        {
            var licence = Configs.GetValue(SystemParameters.NLFRAME_SYSTEM_LICENCE);
            if (string.IsNullOrEmpty(licence))
            {
                licence = Common.GuId();
                Configs.SetValue(SystemParameters.NLFRAME_SYSTEM_LICENCE, licence);
            }
            return Md5.MD5Encrypt(licence, 32);
        }
    }
}
