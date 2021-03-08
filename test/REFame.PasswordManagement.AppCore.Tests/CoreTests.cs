using NUnit.Framework;
using REFame.PasswordManagement.AppCore.Contracts;
using System;

namespace REFame.PasswordManagement.AppCore.Tests
{
    [TestFixture()]
    public class CoreTests
    {

        [Test()]
        public void RegisterParameterLessTypeTest()
        {
            ICore core = new Core();
            core.RegisterType<IInterfaceForSubClass, MyClassForSubClass>();

            Assert.That(() => core.GetRegisteredType<IInterfaceForSubClass>(), Throws.Nothing);
            Assert.That(core.GetRegisteredType<IInterfaceForSubClass>(), Is.Not.Null);

        }

        [Test]
        public void GetRegisteredTypeCanCreateParameterLessType()
        {
            ICore core = new Core();

            var result = core.GetRegisteredType<MyClassForSubClass>();

            Assert.That(result, Is.Not.Null);
        }

        [Test()]
        public void RegisterParameterFullTypeTest()
        {
            ICore core = new Core();
            core.RegisterType<IInterface, MyClass>();
            core.RegisterType<IInterfaceForSubClass, MyClassForSubClass>();

            Assert.That(() => core.GetRegisteredType<IInterface>(), Throws.Nothing);
            Assert.That(core.GetRegisteredType<IInterface>(), Is.Not.Null);
        }

        [Test]
        public void GetRegisteredTypeThrowsInvalidOperationException()
        {
            ICore core = new Core();

            Assert.That(() => core.GetRegisteredType<IInterface>(), Throws.InvalidOperationException);
        }

        [Test]
        public void GetRegisteredTypeThrowsApplicationException()
        {
            ICore core = new Core();
            core.RegisterType<IInterface, MyClass>();
            
            Assert.That(() => core.GetRegisteredType<IInterface>(), Throws.TypeOf<ApplicationException>());
        }
    }

    interface IInterface
    {

    }

    interface IInterfaceForSubClass
    {

    }

    class MyClass : IInterface
    {
        public MyClass(IInterfaceForSubClass parameter)
        {

        }
    }

    class MyClassForSubClass : IInterfaceForSubClass
    {
        public MyClassForSubClass()
        {

        }
    }

}