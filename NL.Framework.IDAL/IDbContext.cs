//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 19:17:38
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace NL.Framework.IDAL
{
    public partial interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseModel;

        List<TEntity> GetLists<TEntity>(string sql);

        List<TEntity> GetLists<TEntity>(string sql, params SqlParameter[] sqlParameters);

        #region 事务
        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        int Commit();
        /// <summary>
        /// 事务回滚
        /// </summary>
        void Rollback();

        int UsingTransaction(Action<IDbContext> action);
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        IQueryable GetLists<TEntity>() where TEntity : BaseModel;
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable GetLists<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : BaseModel;
        /// <summary>
        /// 查询数据-分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable GetLists<TEntity>(int pageIndex, int pageSize, out int totalCount, Expression<Func<TEntity, bool>> whereLambda) where TEntity : BaseModel;
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
        IQueryable GetLists<TEntity>(int pageIndex, int pageSize, out int totalCount, Expression<Func<TEntity, bool>> whereLambda, bool isAsc, Expression<Func<TEntity, bool>> orderByLambda) where TEntity : BaseModel;

        #endregion

        #region 获取数据模型

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        TEntity GetEntity<TEntity>(Guid fid) where TEntity : BaseModel;
        /// <summary>
        /// 通过表达式获取单个实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        TEntity GetEntity<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : BaseModel;
        #endregion

        #region 数据操作通用方法
        /// <summary>
        /// 通过表达式判断是都存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool IsExist<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : BaseModel;
        /// <summary>
        /// 通过主键判断是否存在
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        bool IsExist<TEntity>(Guid fid) where TEntity : BaseModel;
        /// <summary>
        /// 通过主键进行删除
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        int Delete<TEntity>(Guid fid) where TEntity : BaseModel;
        /// <summary>
        /// 通过表达式进行删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        int Delete<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : BaseModel;
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="ent"></param>
        /// <returns></returns>
        int Update<TEntity>(TEntity ent) where TEntity : BaseModel;
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert<TEntity>(TEntity entity) where TEntity : BaseModel;
        /// <summary>
        /// 插入多条数据
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        int Insert<TEntity>(List<TEntity> entitys) where TEntity : BaseModel;
        /// <summary>
        /// 数据提交
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        #endregion

        #region
        bool ExecuteSqlCommand(string sql,params object[] paras);
        bool ExcuteSqlCommandAsync(string sql, params object[] paras);
        #endregion
    }
}
