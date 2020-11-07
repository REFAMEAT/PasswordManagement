using NUnit.Framework;

namespace REFame.PasswordManagement.Settings.Tests
{
    [TestFixture]
    public class StyleSettingTests
    {
        [SetUp]
        public void Setup()
        {
            var databaseSettingServiceMock = new StyleSettingServiceMock();
            databaseSettingServiceMock.Saved += (sender, e) => savedCalled = true;

            setting = new StyleSetting(databaseSettingServiceMock);
        }

        private StyleSetting setting;
        private bool savedCalled;

        [Test]
        public void LoadTest()
        {
            setting.Load();

            Assert.That(setting.AllowedColors, Is.Not.Empty);
            Assert.That(setting.LanguageItems, Is.Not.Empty);
            Assert.That(setting.SelectedColor, Is.Not.Null);
            Assert.That(setting.SelectedLanguage, Is.Not.Null);
            Assert.That(setting.ThemeItems, Is.Not.Empty);
            Assert.That(setting.SelectedTheme, Is.Not.Null);
        }

        [Test]
        public void SaveTest()
        {
            setting.Load();

            setting.SelectedTheme = "Dark";
            setting.SelectedLanguage = "English";
            setting.SelectedColor = "Blue";

            setting.Save();

            Assert.That(savedCalled, Is.True);
            Assert.That(setting.SelectedTheme, Is.EqualTo("Dark"));
            Assert.That(setting.SelectedLanguage, Is.EqualTo("English"));
            Assert.That(setting.SelectedColor, Is.EqualTo("Blue"));
        }
    }
}