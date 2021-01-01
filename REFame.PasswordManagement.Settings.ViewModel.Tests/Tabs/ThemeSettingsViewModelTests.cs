using System;
using MaterialDesignThemes.Wpf;
using Moq;
using NUnit.Framework;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.Settings.ViewModel.Tabs;

namespace REFame.PasswordManagement.Settings.ViewModel.Tests.Tabs
{
    [TestFixture(TestOf = typeof(ThemeSettingsViewModel))]
    public class ThemeSettingsViewModelTests
    {
        public static ThemeData mockData;
        Mock<ISettingService<ThemeData>> mock = new Mock<ISettingService<ThemeData>>();

        [SetUp]
        public void Setup()
        {
            mockData = new ThemeData()
            {
                Language = "de-de",
                PrimaryColor = "Blue",
                SecondaryColor = "Blue",
                Theme = BaseTheme.Dark
            };

            mock
                .Setup(x => x.Load())
                .Returns(async() => mockData);

            mock
                .Setup(x => x.Save(It.IsAny<ThemeData>()))
                .Callback<ThemeData>(x => mockData = x);
        }

        [Test]
        public void ViewModelDataTest()
        {
            var viewModel = new ThemeSettingsViewModel(mock.Object);

            Assert.That(viewModel.SelectedLanguage, Is.EqualTo(mockData.Language.ToString()));
            Assert.That(viewModel.SelectedColor, Is.EqualTo(mockData.PrimaryColor));
            Assert.That(viewModel.SelectedTheme, Is.EqualTo(mockData.Theme.ToString()));
        }

        [Test]
        public void ViewModelLoadTest()
        {
            var viewModel = new ThemeSettingsViewModel(mock.Object)
            {
                SelectedLanguage = "English",
                SelectedColor = "Black",
                SelectedTheme = "Light",
            };

            viewModel.SettingMediatorOnSaveRequested(null, EventArgs.Empty);

            var viewModel2 = new ThemeSettingsViewModel(mock.Object);

            Assert.That(viewModel2.SelectedLanguage, Is.EqualTo("English"));
            Assert.That(viewModel2.SelectedColor, Is.EqualTo("Black"));
            Assert.That(viewModel2.SelectedTheme, Is.EqualTo("Light"));
        }
    }
}