using System;

namespace REFame.PasswordManagement.AppCore.Contracts
{
    public interface ICore
    {
        TInterface GetRegisteredType<TInterface>();
        object Resolve(Type type);

        void RegisterType<TInterface, TImplementation>() where TImplementation : TInterface;
        void RegisterSingleton<T>(T value);

        ICoreInformation CoreInformation { get; set; }
    }
}