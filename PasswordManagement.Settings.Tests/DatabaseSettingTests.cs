using NUnit.Framework;
using PasswordManagement.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using PasswordManagement.Model.Setting;

namespace PasswordManagement.Settings.Tests
{
    [TestFixture()]
    public class DatabaseSettingTests
    {
        private DatabaseSetting setting;
        private bool savedCalled;

        [SetUp]
        public void Setup()
        {
            var databaseSettingServiceMock = new DatabaseSettingServiceMock();
            databaseSettingServiceMock.Saved += (sender, e) => savedCalled = true;

            setting = new DatabaseSetting(databaseSettingServiceMock);
        }

        [Test()]
        public void LoadTest()
        {
            setting.Load();

            Assert.That(setting.IntegratedSecurity, Is.EqualTo(DatabaseSettingServiceMock.Mocks.IntegratedSecurity));
            Assert.That(setting.Password, Is.EqualTo(DatabaseSettingServiceMock.Mocks.Password));
            Assert.That(setting.DatabaseName, Is.EqualTo(DatabaseSettingServiceMock.Mocks.DatabaseName));
            Assert.That(setting.UseDatabase, Is.EqualTo(DatabaseSettingServiceMock.Mocks.UseDatabase));
            Assert.That(setting.ServerName, Is.EqualTo(DatabaseSettingServiceMock.Mocks.ServerName));
        }

        [Test]
        public void SaveTest()
        {
            setting.Save();

            Assert.That(savedCalled, Is.True);
        }
    }
}