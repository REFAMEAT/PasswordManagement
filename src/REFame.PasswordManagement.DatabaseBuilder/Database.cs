using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace REFame.PasswordManagement.DatabaseBuilder
{
    public class Database
    {
        public void Build<T>(bool deleteExisting,
            DbContextOptions options,
            params Assembly[] assemblies) where T : class
        {
            BuilderContext<T> context = new BuilderContext<T>(assemblies, options);

            if (deleteExisting)
            {
                context.Database.EnsureDeleted();
            }

            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}