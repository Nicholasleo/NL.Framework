//***********************************************************
//    作者：Nicholas Leo
//    E-Mail:nicholasleo1030@163.com
//    GitHub:https://github.com/nicholasleo
//    时间：2019-07-25 18:51:05
//    说明：
//    版权所有：个人
//***********************************************************
using NL.Framework.Common.Log;
using NL.Framework.IDAL;
using NL.Framework.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NL.Framework.DAL
{
    public class NLFrameContext : DbContext, IDbContext
    {
        private readonly ILogger _log;

        public NLFrameContext(ILogger logger) : base("name=DbConn")
        {
            this._log = logger;
            try
            {
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NLFrameContext>());
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"连接数据库失败{typeof(NLFrameContext).Name}", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"连接数据库失败{typeof(NLFrameContext).Name}", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual int Delete<TEntity>(Guid fid) where TEntity : BaseModel
        {
            try
            {
                TEntity t = this.Set<TEntity>().Find(fid);
                this.Set<TEntity>().Remove(t);
                return this.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"Delete<{typeof(TEntity).Name}>失败", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"Delete<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual int Delete<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : BaseModel
        {
            try
            {
                List<TEntity> t = this.Set<TEntity>().Where(whereLambda).ToList();
                t.ForEach(m => this.Entry<TEntity>(m).State = EntityState.Deleted);
                //this.Set<TEntity>().Remove(t);
                return this.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"Delete<{typeof(TEntity).Name}>失败(Lamand)", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"Delete<{typeof(TEntity).Name}>失败(Lamand)", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual TEntity GetTEntity<TEntity>(Guid fid) where TEntity : BaseModel
        {
            TEntity t;
            try
            {
                return this.Set<TEntity>().Find(fid);
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"GetTEntity<{typeof(TEntity).Name}>失败", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"GetTEntity<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual TEntity GetTEntity<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : BaseModel
        {
            try
            {
                return this.Set<TEntity>().Where(where).FirstOrDefault();
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"GetTEntity<{typeof(TEntity).Name}>失败(Lamand)", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"GetTEntity<{typeof(TEntity).Name}>失败(Lamand)", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual IQueryable GetLists<TEntity>() where TEntity : BaseModel
        {
            try
            {
                return this.Set<TEntity>();
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual IQueryable GetLists<TEntity>(Expression<Func<TEntity, bool>> whereLambda) where TEntity : BaseModel
        {
            try
            {
                return this.Set<TEntity>().Where(whereLambda);
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败(Lamand)", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败(Lamand)", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual IQueryable GetLists<TEntity>(int pageIndex, int pageSize, out int totalCount, Expression<Func<TEntity, bool>> whereLambda) where TEntity : BaseModel
        {
            try
            {
                if (whereLambda == null)
                {
                    totalCount = this.Set<TEntity>().Count();
                    return this.Set<TEntity>().OrderBy(t => t.Fid).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                }
                else
                {
                    totalCount = this.Set<TEntity>().Where(whereLambda).Count();
                    return this.Set<TEntity>().Where(whereLambda).OrderBy(t => t.Fid).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                }
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败(PageLamand)", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败(PageLamand)", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual IQueryable GetLists<TEntity>(int pageIndex, int pageSize, out int totalCount, Expression<Func<TEntity, bool>> whereLambda, bool isAsc, Expression<Func<TEntity, bool>> orderByLambda) where TEntity : BaseModel
        {
            try
            {
                if (whereLambda == null)
                {
                    totalCount = this.Set<TEntity>().Count();
                    return this.Set<TEntity>().OrderBy(t => t.Fid).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                }
                else
                {
                    if (orderByLambda == null)
                    {
                        totalCount = this.Set<TEntity>().Where(whereLambda).Count();
                        return this.Set<TEntity>().OrderBy(t => t.Fid).Where(whereLambda).OrderBy(t => t.Fid).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                    }
                    else
                    {
                        totalCount = this.Set<TEntity>().Where(whereLambda).Count();
                        if (isAsc)
                            return this.Set<TEntity>().Where(whereLambda).OrderBy(orderByLambda).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                        else
                            return this.Set<TEntity>().Where(whereLambda).OrderByDescending(orderByLambda).Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败(PageLamand)", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败(PageLamand)", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual int Insert<TEntity>(TEntity ent) where TEntity : BaseModel
        {
            try
            {
                this.Entry<TEntity>(ent).State = EntityState.Added;
                return this.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"Insert<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"Insert<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual int Insert<TEntity>(List<TEntity> entitys) where TEntity : BaseModel
        {
            try
            {
                foreach (var item in entitys)
                {
                    this.Entry<TEntity>(item).State = EntityState.Added;
                }
                return this.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"InsertList<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"InsertList<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual bool IsExist<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : BaseModel
        {
            try
            {
                return this.Set<TEntity>().Any(where);
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"IsExist<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"IsExist<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual bool IsExist<TEntity>(Guid fid) where TEntity : BaseModel
        {
            try
            {
                return this.Set<TEntity>().Find(fid) != null;
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"IsExist<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"IsExist<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual int Update<TEntity>(TEntity ent) where TEntity : BaseModel
        {
            try
            {
                this.Set<TEntity>().Attach(ent);
                this.Entry<TEntity>(ent).State = EntityState.Modified;
                return this.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"Update<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"Update<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var item in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(item);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);

        }

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : BaseModel
        {
            return base.Set<TEntity>();
        }

        public virtual TEntity GetEntity<TEntity>(Guid fid) where TEntity : BaseModel
        {
            try
            {
                return this.Set<TEntity>().Find(fid);
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"GetEntity<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"GetEntity<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
        }

        public virtual TEntity GetEntity<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : BaseModel
        {
            try
            {
                return this.Set<TEntity>().Where(where).FirstOrDefault();
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"GetEntity<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"GetEntity<{typeof(TEntity).Name}>失败", ex);
                throw new Exception(ex.Message);
            }
        }

        public List<TEntity> GetLists<TEntity>(string sql)
        {
            try
            {
                return this.Database.SqlQuery<TEntity>(sql).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败(List)", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败(List)", ex);
                throw new Exception(ex.Message);
            }
        }

        public List<TEntity> GetLists<TEntity>(string sql, params SqlParameter[] sqlParameters)
        {
            try
            {
                return this.Database.SqlQuery<TEntity>(sql).ToList();
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败(List)", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"GetLists<{typeof(TEntity).Name}>失败(List)", ex);
                throw new Exception(ex.Message);
            }
        }

        #region 事务
        DbContextTransaction Transaction = null;
        public void BeginTransaction()
        {
            if (Transaction == null)
                this.Database.BeginTransaction();
        }

        public int Commit()
        {
            try
            {
                int result = 0;
                if (Transaction == null)
                {
                    result += this.SaveChanges();

                    DbContextTransaction transaction = this.Database.CurrentTransaction;
                    if (transaction != null)
                    {
                        transaction.Commit();
                        transaction.Dispose();
                        result += 1;
                    }
                }
                return result;
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"Commit", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"Commit", ex);
                throw new Exception(ex.Message);
            }
        }

        public void Rollback()
        {
            try
            {
                if (Transaction == null)
                {

                    DbContextTransaction transaction = this.Database.CurrentTransaction;
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        transaction.Dispose();
                    }
                }
                else
                {
                    throw new Exception("事务异常");
                }
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"Rollback失败", ex);
                throw new DbEntityValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"Rollback失败", ex);
                throw new Exception(ex.Message);
            }
        }
        public int UsingTransaction(Action<IDbContext> action)
        {
            using (var tran = this.Database.BeginTransaction())
            {
                try
                {
                    action.Invoke(this);
                    tran.Commit();
                    return 1;
                }
                catch (DbEntityValidationException ex)
                {
                    tran.Rollback();
                    _log.Error($"UsingTransaction失败", ex);
                    return 0;
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _log.Error($"UsingTransaction失败", ex);
                    return 0;
                    throw new Exception(ex.Message);
                }
            }
        }

        public int UsingTransaction<T>(Func<IDbContext, T> func)
        {
            using (var tran = this.Database.BeginTransaction())
            {
                try
                {
                    func.Invoke(this);
                    tran.Commit();
                    return 1;
                }
                catch (DbEntityValidationException ex)
                {
                    tran.Rollback();
                    _log.Error($"UsingTransaction失败", ex);
                    return 0;
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _log.Error($"UsingTransaction失败", ex);
                    return 0;
                    throw new Exception(ex.Message);
                }
            }
        }

        #endregion


        public bool ExecuteSqlCommand(string sql, params object[] paras)
        {
            try
            {
                int i = this.Database.ExecuteSqlCommand(sql, paras);
                return i > 0 ? true : false;
            }
            catch (DbEntityValidationException ex)
            {
                _log.Error($"ExecuteSqlCommand失败：{sql}", ex);
                return false;
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _log.Error($"ExecuteSqlCommand失败：{sql}", ex);
                return false;
                throw new Exception(ex.Message);
            }
        }

        public bool ExcuteSqlCommandAsync(string sql, params object[] paras)
        {
            throw new NotImplementedException();
        }
    }
}
