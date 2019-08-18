//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-10 11:26:00
//    说明：
//    版权所有：个人
//***********************************************************

namespace NL.Framework.Common
{
    /// <summary>
    /// 系统配置参数
    /// </summary>
    public static class SystemParameters
    {
        /// <summary>
        /// 登陆TOKEN标志
        /// </summary>
        public const string NLFRAME_LOGIN_TOKEN = "NLFRAME_LOGIN_TOKEN";

        /// <summary>
        /// 登陆提供者模式
        /// </summary>
        public const string NLFRAME_LOGIN_PROVIDER = "LoginProvider";

        /// <summary>
        /// 系统版本号
        /// </summary>
        public const string NLFRAME_SYSTEM_VERSION = "Version";

        /// <summary>
        /// 系统名称
        /// </summary>
        public const string NLFRAME_SYSTEM_NAME = "SoftName";

        /// <summary>
        /// 系统授权Licence
        /// </summary>
        public const string NLFRAME_SYSTEM_LICENCE = "LicenceKey";

        /// <summary>
        /// 启用系统日志
        /// </summary>
        public const string NLFRAME_SYSTEM_LOG = "IsLog";

        /// <summary>
        /// 启用系统调试
        /// </summary>
        public const string NLFRAME_SYSTEM_DEBUG = "IsDebug";

        /// <summary>
        /// IP过滤
        /// </summary>
        public const string NLFRAME_SYSTEM_IPFILTER = "IsIPFilter";

        /// <summary>
        /// 数据库超时间
        /// </summary>
        public const string NLFRAME_SYSTEM_COMMANDTIMEOUT = "CommandTimeout";

        /// <summary>
        /// 登陆用户KEY
        /// </summary>
        public const string NLFRAME_LOGIN_USER_TOKEN = "NLFRAME_LOGIN_USER_TOKEN";

        /// <summary>
        /// 读取网卡MAC
        /// </summary>
        public const string NLFRAME_SYSTEM_MAC = "NLFRAME_MAC";

        /// <summary>
        /// 授权Cookie
        /// </summary>
        public const string NLFRAME_SYSTEM_LICENCE_COOKIE = "NLFRAME_LICENCE";

        /// <summary>
        /// DES加密、解密KEY
        /// </summary>
        public const string NLFRAME_DESENCRYPT_DESKEY = "NLFRAME_DESENCRYPT_2019";
        /// <summary>
        /// 头像上传路径配置
        /// </summary>
        public const string NLFRAME_SYSTEM_CONFIG_UPLOAD_USER = "UploadUserImg";
    }
}
