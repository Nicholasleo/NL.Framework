using NL.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace NL.Framework.IDAL
{
    public partial interface IBaseDal<TEntity> where TEntity : BaseModel
    {
        #region 获取数据列表
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetLists();
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetLists(Expression<Func<TEntity, bool>> whereLambda);
        /// <summary>
        /// 查询数据-分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetLists(int pageIndex, int pageSize, out int totalCount, Expression<Func<TEntity, bool>> whereLambda);
        /// <summary>
        /// 查询数据-分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="isAsc"></param>
        /// <param name="orderByLambda"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetLists<S>(int pageIndex, int pageSize, out int totalCount, Expression<Func<TEntity, bool>> whereLambda, bool isAsc, Expression<Func<TEntity, bool>> orderByLambda);

        #endregion

        #region 获取数据模型

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        TEntity GetEntity(Guid fid);
        /// <summary>
        /// 通过表达式获取单个实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        TEntity GetEntity(Expression<Func<TEntity, bool>> where);
        #endregion

        #region 数据操作通用方法
        /// <summary>
        /// 通过表达式判断是都存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool IsExist(Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 通过主键判断是否存在
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        bool IsExist(Guid fid);
        /// <summary>
        /// 通过主键进行删除
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        int Delete(Guid fid);
        /// <summary>
        /// 通过表达式进行删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        int Delete(Expression<Func<TEntity, bool>> whereLambda);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        int Update(TEntity ent);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(TEntity entity);
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        int Insert(List<TEntity> entitys);
        /// <summary>
        /// 数据提交
        /// </summary>
        /// <returns></returns>
        int SaveChanges(); 
        #endregion
    }
}
