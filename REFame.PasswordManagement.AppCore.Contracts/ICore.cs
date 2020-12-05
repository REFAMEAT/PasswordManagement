using System.Threading.Tasks;

namespace REFame.PasswordManagement.AppCore.Contracts
{
    public interface ICore
    {
        ICore RegisterModule<TModule>() where TModule : IModule, new();

        TInterface GetRegisteredType<TInterface>();

        void RegisterType<TInterface, TImplementation>() where TImplementation : TInterface, new();

        Task Run();
    }
}