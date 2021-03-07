using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using REFame.PasswordManagement.DB.Entities;

namespace REFame.PasswordManagement.DB.Contracts
{
    public interface IPwmDbContext : IDbContext
    {
        public IDataSet<PASSWORDDATA> PASSWORDDATA { get; }
        public IDataSet<USERDATA> USERDATA { get; }
        public IDataSet<USERTHEME> USERTHEME { get; }
    }

    public interface IDbContext : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void AddRange<T>(IEnumerable<T> entities)
            where T : class;

        void RemoveRange<T>(IEnumerable<T> entities)
            where T : class;

        void Add<T>(T entity)
            where T : class;

        void Remove<T>(T entity)
            where T : class;

    }
}