using System;
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
            var result = MessageBox.Show
            ("Do you want to update a new version, if available?", 
                "Update routine",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            var progressBar = appCore.GetRegisteredType<IProgressBar>();
            progressBar.Show();
            await Task.Delay(100);
           
            try
            {
                var manager =
                    await UpdateManager.GitHubUpdateManager("https://github.com/REFAMEAT/PasswordManagement",
                        "PASSWORDMANAGEMENT");

                await manager.UpdateApp(i =>
                {
                    progressBar.SetProgress(i); 
                    
                });
            }
            catch (Exception ex)
            {
                Logger.Current.Get().Error(ex);
            }
            finally
            {
                progressBar.Close();
            }
        }
    }
}
