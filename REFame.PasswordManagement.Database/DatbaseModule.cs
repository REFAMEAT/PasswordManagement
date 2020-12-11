using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Database.DbSet;
using REFame.PasswordManagement.Database.Model;

namespace REFame.PasswordManagement.Database
{
    public class DatbaseModule : IModule
    {
        public Task Initialize(ICore appCore)
        {
            appCore.RegisterType<IDataSet<PASSWORDDATA>, DataSet<PASSWORDDATA>>();
            appCore.RegisterType<IDataSet<USERDATA>, DataSet<USERDATA>>();
            appCore.RegisterType<IDataSet<USERTHEME>, DataSet<USERTHEME>>();

            return Task.CompletedTask;
        }
    }
}