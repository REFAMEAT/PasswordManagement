using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using REFame.PasswordManagement.DB.Contracts;

namespace REFame.PasswordManagement.DB
{
    public class DataSet<T> : IDataSet<T> where T : class
    {
        private readonly DbSet<T> dbSet;
        private readonly IQueryable<T> queryable;

        public DataSet(DbSet<T> dbSet)
        {
            this.dbSet = dbSet;
            this.queryable = dbSet;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return queryable.GetEnumerator();
        }

        public Type ElementType => queryable.ElementType;

        public Expression Expression => queryable.Expression;

        public IQueryProvider Provider => queryable.Provider;

        public IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> path)
        {
            return queryable.Include(path);
        }

        public IQueryable<T> AsNoTracking()
        {
            return queryable.AsNoTracking();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void AddRange(IEnumerable<T> entity)
        {
            dbSet.AddRange(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return (dbSet as IAsyncEnumerable<T>).GetAsyncEnumerator(cancellationToken);
        }
    }
}