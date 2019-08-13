//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-07 13:47:46
//    说明：
//    版权所有：个人
//***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.Model
{
    public class LoginEnt
    {
        public string UserCode { get; set; }
        public string Password { get; set; }
    }

    public class AjaxResultEnt
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object ExtMsg { get; set; }
    }

    public class LoginStatusEnt : AjaxResultEnt
    {
        public LoginUserEnt LoginUserEnt { get; set; }
    }

    public class LoginUserEnt
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleCode { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string UserPwd { get; set; }
    }
}
