using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Data.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Get(object id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity tEntity);
        void AddRange(IEnumerable<TEntity> tEntities);
        void Remove(TEntity tEntity);
        void RemoveRange(IEnumerable<TEntity> tEntities);
    }
}
