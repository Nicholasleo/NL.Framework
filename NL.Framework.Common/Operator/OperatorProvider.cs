//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-10 10:28:40
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Common.Config;
using NL.Framework.Common.Json;
using NL.Framework.Common.Security;
using NL.Framework.Model;
using System;

namespace NL.Framework.Common
{
    public class OperatorProvider
    {
        public static OperatorProvider Provider
        {
            get {
                return new OperatorProvider();
            }
        }

        private string LoginUserKey = SystemParameters.NLFRAME_LOGIN_USER_TOKEN;
        public string LoginProvider = Configs.GetValue(SystemParameters.NLFRAME_LOGIN_PROVIDER);

        public LoginUserEnt GetCurrent()
        {
            LoginUserEnt ent = new LoginUserEnt();
            try
            {
                if (LoginProvider == "Cookie")
                {
                    ent = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<LoginUserEnt>();
                }
                else
                {
                    ent = DESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToObject<LoginUserEnt>();
                }
                return ent;
            }
            catch (Exception ex)
            {
                ent = null;
                return ent;
            }
        }

        public void AddCurrent(LoginUserEnt userInfo)
        {
            if (LoginProvider == "Cookie")
            {
                WebHelper.WriteCookie(LoginUserKey, DESEncrypt.Encrypt(userInfo.ToJson()), 60);
            }
            else
            {
                WebHelper.WriteSession(LoginUserKey, DESEncrypt.Encrypt(userInfo.ToJson()));
            }
            WebHelper.WriteCookie(SystemParameters.NLFRAME_SYSTEM_MAC, Md5.MD5Encrypt(Net.GetMacByNetworkInterface().ToJson(), 32));
            WebHelper.WriteCookie(SystemParameters.NLFRAME_SYSTEM_LICENCE_COOKIE, Licence.GetLicence());
        }
        public void RemoveCurrent()
        {
            if (LoginProvider == "Cookie")
            {
                WebHelper.RemoveCookie(LoginUserKey.Trim());
            }
            else
            {
                WebHelper.RemoveSession(LoginUserKey.Trim());
            }
        }
    }
}
