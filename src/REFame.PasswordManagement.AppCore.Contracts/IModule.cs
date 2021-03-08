using System.Threading.Tasks;

namespace REFame.PasswordManagement.AppCore.Contracts
{
    public interface IModule
    {
        Task Initialize(ICore appCore);
    }
}