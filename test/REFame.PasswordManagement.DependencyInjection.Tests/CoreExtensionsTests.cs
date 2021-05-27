using System;
using System.Collections.Generic;
using NUnit.Framework;
using REFame.PasswordManagement.AppCore.Contracts;

namespace REFame.PasswordManagement.DependencyInjection.Tests
{
    public class CoreExtensionsTests
    {
        [Test]
        public void RegisterTypesShouldRegisterTypes()
        {
            var core = new MockCore();

            core.RegisterTypes();

            Assert.That(core.List, Is.Not.Empty);
        }
    }

    class MockCore : ICore
    {
        public readonly List<Type> List = new List<Type>();

        public TInterface GetRegisteredType<TInterface>()
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type type)
        {
            throw new NotImplementedException();
        }

        public void RegisterType<TInterface, TImplementation>() where TImplementation : TInterface
        {
            List.Add(typeof(TInterface));
        }

        public void RegisterSingleton<T>(T value)
        {
            throw new NotImplementedException();
        }

        public ICoreInformation CoreInformation { get; set; }
    }
}