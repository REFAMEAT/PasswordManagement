using NUnit.Framework;

namespace REFame.PasswordManagement.File.Tests
{
    public class FolderProviderTests
    {
        [Test]
        public void AppDataFolderReturnsPath()
        {
            FolderProvider folderProvider = new FolderProvider();

            string folder = folderProvider.AppDataFolder;

            Assert.That(folder, Is.Not.Empty);
            Assert.That(folder, Contains.Substring("PasswordManagement"));
            Assert.That(folder, Contains.Substring("REFame"));
        }
    }
}