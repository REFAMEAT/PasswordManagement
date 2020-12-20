using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using Squirrel;

namespace REFame.PasswordManagement.Update
{
    public class UpdateModule : IModule
    {
        public async Task Initialize(ICore appCore)
        {
            string appPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "PASSWORDMANAGEMENT");

            using var manager = new UpdateManager(appPath);
            await manager.UpdateApp();
        }
    }
}
