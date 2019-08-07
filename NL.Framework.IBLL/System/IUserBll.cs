using NL.Framework.Model.NLFrameEnt;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;

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
    public interface IUserBll: ISystemBaseBll,IBaseBll
    {
        List<UserModel> GetUserLists(int page, int limit, out int total, UserPageEnt pageEnt);

        UserModel GetUserModel(Guid fid);
        UserEditEnt GetUserEidtModel(Guid fid);
        UserRoleModel GetUserRoleModel(Guid fid);

        IQueryable GetUserAll();

        int AddUser(UserEditEnt model);

        int DeleteUser(Guid fid);

        int UpdateUser(UserEditEnt model);
    }
}
