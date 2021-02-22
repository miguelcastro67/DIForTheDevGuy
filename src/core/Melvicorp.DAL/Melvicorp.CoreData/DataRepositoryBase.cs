using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Melvicorp.CoreData
{
    public abstract class DataRepositoryBase<T, U, V> : IDataRepository<T, U, V>
            where T : class, IIdentifiableEntity<V>, new()
            where U : DbContext, new()
    {
        protected abstract DbSet<T> DbSet(U entityContext);
        protected abstract Expression<Func<T, bool>> IdentifierPredicate(U entityContext, V id);

        T AddEntity(U entityContext, T entity)
        {           
            return DbSet(entityContext).Add(entity).Entity;
        }

        IEnumerable<T> GetEntities(U entityContext)
        {
            return DbSet(entityContext).ToFullyLoaded();
        }

        T GetEntity(U entityContext, V id)
        {
            return DbSet(entityContext).Where(IdentifierPredicate(entityContext, id)).FirstOrDefault();
        }

        T UpdateEntity(U entityContext, T entity)
        {
            var q = DbSet(entityContext).Where(IdentifierPredicate(entityContext, entity.EntityId));
            return q.FirstOrDefault();
        }

        public virtual T Add(T entity)
        {
            using (U entityContext = new U())
            {
                T addedEntity = AddEntity(entityContext, entity);
                entityContext.SaveChanges();
                return addedEntity;
            }
        }

        public virtual void Remove(T entity)
        {
            using (U entityContext = new U())
            {
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public virtual void Remove(V id)
        {
            using (U entityContext = new U())
            {
                T entity = GetEntity(entityContext, id);
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                entityContext.SaveChanges();
            }
        }

        public virtual T Update(T entity)
        {
            using (U entityContext = new U())
            {
                T existingEntity = UpdateEntity(entityContext, entity);

                SimpleMapper.PropertyMap(entity, existingEntity);

                entityContext.SaveChanges();
                return existingEntity;
            }
        }

        public virtual IEnumerable<T> Get()
        {
            using (U entityContext = new U())
                return (GetEntities(entityContext)).ToArray().ToList();
        }

        public virtual T Get(V id)
        {
            using (U entityContext = new U())
                return GetEntity(entityContext, id);
        }
        
        //public T ExecuteSql(string sql, List<DbParameter> parms)
        //{
        //    using (U entityContext = new U())
        //    {
        //        IDataReader reader = ExecuteSql(entityContext, sql, parms);
        //        
        //        var result = ((IObjectContextAdapter)entityContext).ObjectContext.Translate<T>(reader);
        //        return result.FirstOrDefault();
        //    }
        //}

        public IDataReader ExecuteSql(U entityContext, string sql, List<DbParameter> parms)
        {
            DbConnection connection = entityContext.Database.GetDbConnection();
            DbCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            if (parms != null)
                foreach (DbParameter p in parms)
                    command.Parameters.Add(p);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            IDataReader reader = command.ExecuteReader();
            return reader;
        }
    }
}
