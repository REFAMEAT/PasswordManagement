using System;
using Moq;
using NUnit.Framework;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.Settings.SettingFactories;
using REFame.PasswordManagement.Settings.ViewModel.Tabs;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.Tests.SettingFactories
{
    [TestFixture]
    public class DatabaseSettingsFactoryTests
    {
        private static DatabaseData mockData;
        private Mock<ISettingService<DatabaseData>> mock = new Mock<ISettingService<DatabaseData>>();

        [SetUp]
        public void Setup()
        {
            mockData = new DatabaseData
            {
                Username = "MockUserName",
                Password = "MockPassword",
                DatabaseName = "MockDatabaseName",
                ServerName = "MockServerName",
                IntegratedSecurity = false,
                UseDatabase = true
            };

            mock
                .Setup(x => x.Load())
                .Returns(async () =>  mockData);

            mock
                .Setup(x => x.Save(It.IsAny<DatabaseData>()))
                .Callback<DatabaseData>(x => mockData = x);
        }

        [Test]
        public void GetViewModelTest()
        {
            DatabaseSettingsFactory databaseSettingsFactory = new DatabaseSettingsFactory();
            databaseSettingsFactory.OverrideSettingService = mock.Object;

            var viewModel = databaseSettingsFactory.GetViewModel() as DatabaseSettingsViewModel;

            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void GetMediatorThrowsNullReference()
        {
            DatabaseSettingsFactory databaseSettingsFactory = new DatabaseSettingsFactory();
            databaseSettingsFactory.OverrideSettingService = mock.Object;

            // View-Model is not yet setted
            Assert.That(() => databaseSettingsFactory.GetMediator(), 
                Throws.Exception.TypeOf(typeof(NullReferenceException)));
        }

        [Test]
        public void GetMediator()
        {
            DatabaseSettingsFactory databaseSettingsFactory = new DatabaseSettingsFactory();
            databaseSettingsFactory.OverrideSettingService = mock.Object;
            databaseSettingsFactory.GetViewModel();

            SettingMediator mediator = databaseSettingsFactory.GetMediator();
            
            Assert.That(mediator, Is.Not.Null);
        }
    }
}