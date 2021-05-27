using System;
using System.Globalization;
using System.Windows;
using NUnit.Framework;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.AppCore.Tests.Mock;

namespace REFame.PasswordManagement.AppCore.Tests
{
    public class CoreBuilderTests
    {
        [Test]
        public void BuildShouldReturnCoreWithInfosButEmpty()
        {
            CoreBuilder coreBuilder = new CoreBuilder(new Core());

            coreBuilder.BuildCore();

            Assert.That(coreBuilder.Info.AppCultureInfo, Is.Null);
            Assert.That(coreBuilder.Info.MainViewModel, Is.Null);
            Assert.That(coreBuilder.Info.MainWindow, Is.Null);
            Assert.That(coreBuilder.Info.LoginHandler, Is.Null);
            Assert.That(coreBuilder.Info.ThemeData, Is.Null);
        }

        [Test]
        public void UseCultureInitializesCultureInfo()
        {
            CoreBuilder coreBuilder = new CoreBuilder(new Core());

            coreBuilder.UseCulture<MockCultureBuilder>();
            coreBuilder.BuildCore();

            Assert.That(coreBuilder.Info.AppCultureInfo, Is.Not.Null);
            Assert.That(coreBuilder.Info.AppCultureInfo.LCID, Is.EqualTo(new CultureInfo("de-de").LCID));
            Assert.That(coreBuilder.Info.MainViewModel, Is.Null);
            Assert.That(coreBuilder.Info.MainWindow, Is.Null);
            Assert.That(coreBuilder.Info.LoginHandler, Is.Null);
            Assert.That(coreBuilder.Info.ThemeData, Is.Null);
        }

        [Test]
        public void UseLoginInitializesLogin()
        {
            CoreBuilder coreBuilder = new CoreBuilder(new Core());

            coreBuilder.UseLogin<MockLoginHandler>();
            coreBuilder.BuildCore();

            Assert.That(coreBuilder.Info.AppCultureInfo, Is.Null);
            Assert.That(coreBuilder.Info.MainViewModel, Is.Null);
            Assert.That(coreBuilder.Info.MainWindow, Is.Null);
            Assert.That(coreBuilder.Info.LoginHandler, Is.Not.Null);
            Assert.That(coreBuilder.Info.LoginHandler, Is.TypeOf<MockLoginHandler>());
            Assert.That(coreBuilder.Info.ThemeData, Is.Null);
        }

        [Test]
        public void UseUIInitializesUI()
        {
            CoreBuilder coreBuilder = new CoreBuilder(new Core());

            coreBuilder.UseUI<MockUIBuilder>();
            coreBuilder.BuildCore();

            Assert.That(coreBuilder.Info.AppCultureInfo, Is.Null);
            Assert.That(coreBuilder.Info.MainViewModel, Is.Null);
            Assert.That(coreBuilder.Info.MainWindow, Is.Null);
            Assert.That(coreBuilder.Info.LoginHandler, Is.Null);
            Assert.That(coreBuilder.Info.ThemeData, Is.Not.Null);
        }

        [Test]
        public void MainViewModelInitializesMainViewModel()
        {
            CoreBuilder coreBuilder = new CoreBuilder(new Core());

            coreBuilder.MainViewModel<Random>();
            coreBuilder.BuildCore();

            Assert.That(coreBuilder.Info.AppCultureInfo, Is.Null);
            Assert.That(coreBuilder.Info.MainViewModel, Is.Not.Null);
            Assert.That(coreBuilder.Info.MainViewModel, Is.EqualTo(typeof(Random)));
            Assert.That(coreBuilder.Info.MainWindow, Is.Null);
            Assert.That(coreBuilder.Info.LoginHandler, Is.Null);
            Assert.That(coreBuilder.Info.ThemeData, Is.Null);
        }

        [Test]
        public void MainWindowInitializesMainWindow()
        {
            CoreBuilder coreBuilder = new CoreBuilder(new Core());

            coreBuilder.MainWindow<Window>();
            coreBuilder.BuildCore();

            Assert.That(coreBuilder.Info.AppCultureInfo, Is.Null);
            Assert.That(coreBuilder.Info.MainViewModel, Is.Null);
            Assert.That(coreBuilder.Info.MainWindow, Is.Not.Null);
            Assert.That(coreBuilder.Info.MainWindow, Is.EqualTo(typeof(Window)));
            Assert.That(coreBuilder.Info.LoginHandler, Is.Null);
            Assert.That(coreBuilder.Info.ThemeData, Is.Null);
        }
    }
}