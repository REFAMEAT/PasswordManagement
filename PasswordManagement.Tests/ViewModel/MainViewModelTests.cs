using NUnit.Framework;
using PasswordManagement.Backend.Security;
using PasswordManagement.Model;
using PasswordManagement.ViewModel;

namespace PasswordManagement.Tests.ViewModel
{
    [TestFixture]
    public class MainViewModelTests
    {
        private MainViewModel viewModel;
        private TestDataManager dataManager;

        [SetUp]
        public void Setup()
        {
            dataManager = new TestDataManager();

            dataManager.passwordDatas.Add(new PasswordData()
            {
                Comments = "TEST",
                Description = "TEST",
                Identifier = "TEST",
                Password = Encryption.EncryptString("SafePassword", "testPW"),
            });
            App.loginPw = "testPW";

            viewModel = new MainViewModel(dataManager);
        }

        [Test]
        public void TestDeleteItem()
        {
            PasswordDataDisplay passwordDataDisplay = new PasswordDataDisplay(dataManager.passwordDatas[0]);

            viewModel.SelectedItem = new PasswordDataDisplay(passwordDataDisplay);
            viewModel.ButtonCommandDeleteItem.Execute(null);

            Assert.That(dataManager.passwordDatas.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestDeleteNullItem()
        {
            viewModel.ButtonCommandDeleteItem.Execute(null);
            Assert.That(dataManager.passwordDatas.Count, Is.EqualTo(1));
        }
    }
}