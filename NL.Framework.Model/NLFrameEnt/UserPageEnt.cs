using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-03 16:32:46 
//    说明：
//    版权所有：个人
//***********************************************************
namespace NL.Framework.Model.NLFrameEnt
{
    public class UserPageEnt
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
    }

    public class UserRoleEnt
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }

    public class UserEditEnt : BaseModel
    {
        public Guid RoleId { get; set; }
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
        /// <summary>
        /// 用户头像路径
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
