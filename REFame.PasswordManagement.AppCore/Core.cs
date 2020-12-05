using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;

namespace REFame.PasswordManagement.AppCore
{
    public class Core : ICore
    {
        private Dictionary<Type, Func<object>> mapping;
        private List<IModule> modules;

        internal Core()
        {
            this.modules = new List<IModule>();
            mapping = new Dictionary<Type, Func<object>>();
        }

        public ICore RegisterModule<TModule>() where TModule : IModule, new()
        {
            modules.Add(new TModule());
            return this;
        }

        public TInterface GetRegisteredType<TInterface>()
        {
            object value = Resolve(typeof(TInterface));

            return value is TInterface o ? o : default;
        }

        public void RegisterType<TInterface, TImplementation>() where TImplementation : TInterface, new()
        {
            mapping.TryAdd(typeof(TInterface), () => new TImplementation());
        }

        public async Task Run()
        {
            foreach (IModule module in modules)
            {
                await module.Initialize(this);
            }
        }

        private object Resolve(Type type)
        {
            mapping.TryGetValue(type, out var func);

            return func?.Invoke();
        }
    }
}