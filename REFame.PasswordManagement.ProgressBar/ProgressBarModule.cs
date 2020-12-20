using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.ProgressBar.Contracts;
using REFame.PasswordManagement.ProgressBar.View;

namespace REFame.PasswordManagement.ProgressBar
{
    public class ProgressBarModule : IModule
    {
        public Task Initialize(ICore appCore)
        {
            appCore.RegisterType<IProgressBar, Contracts.ProgressBar>();
            
            return Task.CompletedTask;
        }
    }
}