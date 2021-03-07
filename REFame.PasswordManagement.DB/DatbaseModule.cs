using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.DB.Contracts;

namespace REFame.PasswordManagement.DB
{
    public class DatbaseModule : IModule
    {
        public Task Initialize(ICore appCore)
        {
            appCore.RegisterType<IConnectionStringBuilder, ConnectionStringBuilder>();
            appCore.RegisterType<IPwmDbContextFactory, PwmDbDbContextFactory>();

            return Task.CompletedTask;
        }
    }
}