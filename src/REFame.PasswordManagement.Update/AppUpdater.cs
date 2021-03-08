using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using REFame.PasswordManagement.Logging;
using REFame.PasswordManagement.ProgressBar.Contracts;
using Squirrel;

namespace REFame.PasswordManagement.Update
{
    public class AppUpdater
    {
        private const string githubUrl = "https://github.com/REFAMEAT/PasswordManagement";
        private const string appName = "PASSWORDMANAGEMENT";

        private readonly IProgressBar progressBar;
        private UpdateManager updateManager;

        public AppUpdater()
        {
            progressBar = new ProgressBar.Contracts.ProgressBar();
        }

        public async Task UpdateApp()
        {
            try
            {
                updateManager = await UpdateManager.GitHubUpdateManager(githubUrl, appName);

                UpdateInfo info = await SearchForUpdates();

                if (Debugger.IsAttached || info.CurrentlyInstalledVersion.Version < info.FutureReleaseEntry.Version)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"A new version of PasswordManagement is available ({info.FutureReleaseEntry.Version}) {Environment.NewLine} Do you want to install it now?",
                        "New Version",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Information,
                        MessageBoxResult.Yes);

                    if (result == MessageBoxResult.Yes)
                    {
                        progressBar.Show();
                        await DownloadUpdates(info);
                        await InstallUpdates(info);
                    }
                }

            }
            catch (Exception e)
            {
                Logger.Current.Get().Error(e);
            }
            finally
            {
                updateManager.Dispose();
                progressBar.Close();
            }
        }

        private async Task<UpdateInfo> SearchForUpdates()
        {
            return await updateManager.CheckForUpdate(progress: progressBar.SetProgress);
        }

        private async Task DownloadUpdates(UpdateInfo info)
        {
            progressBar.SetTitle(TitleType.Downloading);
            await updateManager.DownloadReleases(info.ReleasesToApply, progressBar.SetProgress);
        }

        private async Task InstallUpdates(UpdateInfo updateInfo)
        {
            progressBar.SetTitle(TitleType.Installing);
            await updateManager.ApplyReleases(updateInfo, progressBar.SetProgress);

            UpdateManager.RestartApp();
        }
    }
}