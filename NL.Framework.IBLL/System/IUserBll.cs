using NL.Framework.Model;
using NL.Framework.Model.NLFrameEnt;
using NL.Framework.Model.System;
using System;

//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-08-01 15:01:59 
//    说明：
//    版权所有：个人
//***********************************************************
namespace NL.Framework.IBLL
{
    public interface IUserBll: ISystemBaseBll<UserModel>,IBaseBll
    {
        UserEditEnt GetUserEidtModel(Guid fid);
        UserRoleModel GetUserRoleModel(Guid fid);
        AjaxResultEnt UpdateUserRole(UserRoleEnt ent);
        AjaxResultEnt AddUser(UserEditEnt model);
        AjaxResultEnt UpdateUser(UserEditEnt model);
    }
}
