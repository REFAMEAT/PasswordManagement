using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace REFame.PasswordManagement.DB.Internals
{
    public class DbContextBase
    {
        private readonly Context context;

        internal DbContextBase(Context context)
        {
            this.context = context;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return context.SaveChangesAsync();
        }

        public void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            context.AddRange(entities);
        }

        public void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            context.RemoveRange(entities);
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            context.Remove(entity);
        }
    }
}