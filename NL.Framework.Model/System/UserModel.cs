//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 18:22:11
//    说明：
//    版权所有：个人
//***********************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model.System
{
    /// <summary>
    /// 用户实体模型
    /// </summary>
    public partial class UserModel : SystemBaseModel
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 用户年龄
        /// </summary>
        public int UserAge { get; set; }
        /// <summary>
        /// 用户Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户微信
        /// </summary>
        public string WeChat { get; set; }
        /// <summary>
        /// 用户QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 是否虚拟删除用户
        /// </summary>
        public int IsDelete { get; set; }
        /// <summary>
        /// 是否为管理员
        /// </summary>
        public int IsAdmin { get; set; }
        /// <summary>
        /// 第一次登陆系统的时间
        /// </summary>
        public DateTime FirstLoginTime { get; set; }
        /// <summary>
        /// 最近登陆系统的时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 是否禁用用户
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        [JsonIgnore]
        public virtual UserImageModel UserImage { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRoleModel> UserRoleModels { get; set; }
    }
}
