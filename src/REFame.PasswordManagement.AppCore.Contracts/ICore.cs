namespace REFame.PasswordManagement.AppCore.Contracts
{
    public interface ICore
    {
        TInterface GetRegisteredType<TInterface>();

        void RegisterType<TInterface, TImplementation>() where TImplementation : TInterface;
    }
}