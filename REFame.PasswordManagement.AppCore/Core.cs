using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Logging;

[assembly: InternalsVisibleTo("REFame.PasswordManagement.AppCore.Tests")]

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
            if (GetRegisteredType(typeof(TInterface)) is TInterface result)
            {
                return result;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private object GetRegisteredType(Type type)
        {
            object result = null;

            if (type.IsInterface && mapping.TryGetValue(type, out var func))
            {
                result = func?.Invoke();
            }
            else if (type.IsClass)
            {
                result = Resolve(type);
            }

            return result;
        }

        public void RegisterType<TInterface, TImplementation>() where TImplementation : TInterface
        {
            mapping.TryAdd(typeof(TInterface), () => Resolve(typeof(TImplementation)));
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
            var constructors = type
                .GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .FirstOrDefault();

            if (constructors == null)
            {
                throw new ApplicationException("ctor not found");
            }
            else if (constructors.GetParameters().Length == 0)
            {
                return Activator.CreateInstance(type);
            }
            else
            {
                List<object> parameters = new List<object>();
                foreach (var parameterInfo in constructors.GetParameters())
                {
                    object parameter = GetRegisteredType(parameterInfo.ParameterType);

                    if (parameter is null)
                    {
                        parameter = TryResolveType(parameterInfo.ParameterType);

                        if (parameter is null)
                        {
                            throw new ApplicationException("cannot resolve type");
                        }

                    }

                    parameters.Add(parameter);
                }

                return Activator.CreateInstance(type, parameters.ToArray());
            }
        }

        private object TryResolveType(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch (MissingMethodException missingMethodException)
            {
                Logger.Current?.Get()?.Error(missingMethodException);
            }

            foreach (ConstructorInfo constructorInfo in type.GetConstructors())
            {
                object[] nullParameters = new object[constructorInfo.GetParameters().Length];

                try
                {
                    return Activator.CreateInstance(type, nullParameters);
                }
                catch (Exception)
                {
                    Logger.Current?.Get()?.Information("Tried to create instance");
                }
            }
            return null;
        }
    }
}