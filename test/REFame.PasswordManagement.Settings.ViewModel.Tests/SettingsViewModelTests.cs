using NUnit.Framework;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.ViewModel.Tests
{
    [TestFixture(TestOf = typeof(SettingsViewModel))]
    public class SettingsViewModelTests
    {
        [Test]
        public void TestConstructor()
        {
            var viewModel = new SettingsViewModel();

            Assert.That(viewModel.SettingMediators, Is.Not.Null);
            Assert.That(viewModel.SaveCommand, Is.Not.Null);
        }

        [Test]
        public void TestSaveCommand()
        {
            var viewModel = new SettingsViewModel();

            Assert.That(() => viewModel.SaveCommand.Execute(null), Throws.Nothing);
        }

        [Test]
        public void TestRegisteredMediator()
        {
            bool saveCalled = false;
            SettingMediator mediator = new SettingMediator();
            mediator.SaveRequested += (sender, args) => saveCalled = true;

            var viewModel = new SettingsViewModel();
            viewModel.SettingMediators.Add(mediator);

            viewModel.SaveCommand.Execute(null);

            Assert.That(saveCalled, Is.True);
        }
    }
}