using System;
using MaterialDesignThemes.Wpf;
using Moq;
using NUnit.Framework;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.Settings.SettingFactories;
using REFame.PasswordManagement.Settings.ViewModel.Tabs;
using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Mediator;
using ITheme = REFame.PasswordManagement.Model.Interfaces.ITheme;

namespace REFame.PasswordManagement.Settings.Tests.SettingFactories
{
    [TestFixture(TestOf = typeof(ThemeSettingsFactory))]
    public class ThemeSettingsFactoryTests
    {
        public static ThemeData mockData;
        Mock<ISettingService<ThemeData>> mock = new Mock<ISettingService<ThemeData>>();

        [SetUp]
        public void Setup()
        {
            mockData = new ThemeData()
            {
                Language = Language.Dummy,
                PrimaryColor = "Blue",
                SecondaryColor = "Blue",
                Theme = BaseTheme.Dark
            };

            mock
                .Setup(x => x.Load())
                .Returns(async () => mockData);

            mock
                .Setup(x => x.Save(It.IsAny<ThemeData>()))
                .Callback<ThemeData>(x => mockData = x);
        }

        [Test]
        public void GetViewModelTest()
        {
            ThemeSettingsFactory ThemeSettingsFactory = new ThemeSettingsFactory();
            ThemeSettingsFactory.OverrideSettingService = mock.Object;

            var viewModel = ThemeSettingsFactory.GetViewModel() as ThemeSettingsViewModel;

            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void GetMediatorThrowsNullReference()
        {
            ThemeSettingsFactory ThemeSettingsFactory = new ThemeSettingsFactory();
            ThemeSettingsFactory.OverrideSettingService = mock.Object;

            // View-Model is not yet setted
            Assert.That(() => ThemeSettingsFactory.GetMediator(),
                Throws.Exception.TypeOf(typeof(NullReferenceException)));
        }

        [Test]
        public void GetMediator()
        {
            ThemeSettingsFactory ThemeSettingsFactory = new ThemeSettingsFactory();
            ThemeSettingsFactory.OverrideSettingService = mock.Object;
            ThemeSettingsFactory.GetViewModel();

            SettingMediator mediator = ThemeSettingsFactory.GetMediator();

            Assert.That(mediator, Is.Not.Null);
        }
    }
}