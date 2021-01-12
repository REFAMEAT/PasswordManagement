using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Logging;
using REFame.PasswordManagement.ProgressBar.Contracts;
using Squirrel;

namespace REFame.PasswordManagement.Update
{
    public class UpdateModule : IModule
    {
        public async Task Initialize(ICore appCore)
        {
            if (Debugger.IsAttached)
            {
                return;
            }

            using UpdateManager updateManager = 
                await UpdateManager.GitHubUpdateManager(
                "https://github.com/REFAMEAT/PasswordManagement",
                "PASSWORDMANAGEMENT");

            if (!Environment.GetCommandLineArgs().Contains("--squirrel-updated"))
            {
                SquirrelAwareApp.HandleEvents(
                    v => updateManager.CreateShortcutForThisExe(),
                    v => updateManager.RemoveShortcutForThisExe());
            }

            UpdateInfo updateInfo = await updateManager.CheckForUpdate(true);

            var progress = appCore.GetRegisteredType<IProgressBar>();

            if (updateInfo.CurrentlyInstalledVersion.Version < updateInfo.FutureReleaseEntry.Version)
            {
                if (MessageBox.Show($"New Version is available {updateInfo.FutureReleaseEntry.Version}",
                    "New version", 
                    MessageBoxButton.YesNo, 
                    MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    progress.Show();
                    await Task.Delay(100);

                    await updateManager.DownloadReleases(updateInfo.ReleasesToApply, x => progress.SetProgress(x))
                        .ContinueWith((t) =>
                        {
                            updateManager.ApplyReleases(updateInfo, progress.SetProgress)
                                .ContinueWith(x =>
                                {
                                    UpdateManager.RestartApp();
                                    return false;
                                });
                        });
                }
            }

            progress.Close();
        }
    }
}
