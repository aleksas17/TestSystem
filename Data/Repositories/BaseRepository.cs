using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Data.IRepositories;

namespace Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(DbContext dbContext)
        {
            DbSet = dbContext.Set<TEntity>();
        }

        public TEntity Get(object id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public void Add(TEntity tEntity)
        {
            DbSet.Add(tEntity);
        }

        public void AddRange(IEnumerable<TEntity> tEntities)
        {
            DbSet.AddRange(tEntities);
        }

        public void Remove(TEntity tEntity)
        {
            DbSet.Remove(tEntity);
        }

        public void RemoveRange(IEnumerable<TEntity> tEntities)
        {
            DbSet.RemoveRange(tEntities);
        }
    }
}
