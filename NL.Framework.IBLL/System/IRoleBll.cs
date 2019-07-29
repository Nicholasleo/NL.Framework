//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:50:46
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL.Framework.IBLL
{
    public interface IRoleBll : ISystemBaseBll,IBaseBll
    {
        List<RoleModel> GetRolesLists(int page, int limit, out int total, string role = "");

        RoleModel GetRoleModel(string fid);

        IQueryable GetRoleAll();

        int AddRole(RoleModel model);

        int DeleteRole(RoleModel model);

        int UpdateRole(RoleModel model);
    }
}
