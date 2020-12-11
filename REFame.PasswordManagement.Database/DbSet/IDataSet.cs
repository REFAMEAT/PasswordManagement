using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using REFame.PasswordManagement.Database.Model;

namespace REFame.PasswordManagement.Database.DbSet
{
    public interface IDataSet<T> where T : class
    {
        DbSet<T> Entities { get; set; }
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);
        void Dispose();
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;
        DatabaseFacade Database { get; }
    }
}