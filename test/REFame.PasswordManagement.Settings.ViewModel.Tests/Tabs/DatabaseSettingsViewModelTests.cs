using System;
using Moq;
using NUnit.Framework;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.Settings.ViewModel.Tabs;

namespace REFame.PasswordManagement.Settings.ViewModel.Tests.Tabs
{
    [TestFixture(TestOf = typeof(DatabaseSettingsViewModel))]
    public class DatabaseSettingsViewModelTests
    {
        public static DatabaseData mockData;
        Mock<ISettingService<DatabaseData>> mock = new Mock<ISettingService<DatabaseData>>();

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
                Type = DataBaseType.Mssql
            };

            mock
                .Setup(x => x.Load())
                .Returns(async () => mockData);

            mock
                .Setup(x => x.Save(It.IsAny<DatabaseData>()))
                .Callback<DatabaseData>(x => mockData = x);
        }

        [Test]
        public void ViewModelDataTest()
        {
            var viewModel = new DatabaseSettingsViewModel(mock.Object);

            Assert.That(viewModel.Username, Is.EqualTo(mockData.Username));
            Assert.That(viewModel.Password, Is.EqualTo(mockData.Password));
            Assert.That(viewModel.DatabaseName, Is.EqualTo(mockData.DatabaseName));
            Assert.That(viewModel.ServerName, Is.EqualTo(mockData.ServerName));
            Assert.That(viewModel.IntegratedSecurity, Is.False);
        }

        [Test]
        public void ViewModelLoadTest()
        {
            var viewModel = new DatabaseSettingsViewModel(mock.Object)
            {
                IntegratedSecurity = true,
                Password = "NEWPASSWORD",
                Username = "NEWUSERNAME"
            };
            viewModel.SettingMediatorOnSaveRequested(null, EventArgs.Empty);

            var viewModel2 = new DatabaseSettingsViewModel(mock.Object);

            Assert.That(viewModel2.IntegratedSecurity, Is.True);
            Assert.That(viewModel2.Password, Is.EqualTo("NEWPASSWORD"));
            Assert.That(viewModel2.Username, Is.EqualTo("NEWUSERNAME"));
        }
    }
}