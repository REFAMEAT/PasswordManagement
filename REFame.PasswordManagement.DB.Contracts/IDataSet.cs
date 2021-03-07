using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace REFame.PasswordManagement.DB.Contracts
{
    public interface IDataSet : IQueryable
    {
        
    }

    public interface IDataSet<T> : IDataSet, IQueryable<T>, IAsyncEnumerable<T>
    {
        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path);
        IQueryable<T> AsNoTracking();
        void Add(T entity);
        void Remove(T entity);
        void AddRange(IEnumerable<T> entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}