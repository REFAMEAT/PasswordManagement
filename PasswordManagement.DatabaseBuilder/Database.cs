using System.Reflection;
using System.Threading.Tasks;

namespace PasswordManagement.DatabaseBuilder
{
    public class Database
    {
        public async Task Build<T>(bool deleteExisting, params Assembly[] assemblies) where T : class
        {
            BuilderContext<T> context = new BuilderContext<T>(assemblies ,"MARS", "PasswordManagement");
            
            if (deleteExisting)
            {
                await context.Database.EnsureDeletedAsync();
            }

            await context.Database.EnsureCreatedAsync();
            await context.SaveChangesAsync();
        }
    }
}