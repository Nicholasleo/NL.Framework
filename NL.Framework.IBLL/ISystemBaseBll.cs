//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-29 13:52:35
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model;
using NL.Framework.Model.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NL.Framework.IBLL
{
    public interface ISystemBaseBll<T> where T : BaseModel
    {
        #region 公有的获取菜单权限接口
        List<FunctionModel> GetMenuFunction();
        List<FunctionModel> GetMenuFunction(Guid menuFid, Guid roleFid);
        List<FunctionModel> GetMenuFunction(Guid menuFid, string roleCode);
        List<FunctionModel> GetMenuFunction(string menuName, string roleCode);
        List<FunctionModel> GetMenuFunction(string menuName, Guid roleFid);

        #endregion

        #region CRUD
        /// <summary>
        /// 查询IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable GetQueryable();
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        T GetModel(Guid fid);
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="total"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        List<T> GetLists(int page, int limit, out int total, object obj);
        /// <summary>
        /// 查询列表（所有）
        /// </summary>
        /// <returns></returns>
        List<T> GetLists();
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AjaxResultEnt Create(T model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        AjaxResultEnt Delete(Guid fid);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        AjaxResultEnt Delete(List<T> lists);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AjaxResultEnt Update(T model); 
        #endregion
    }
}
