using System.Diagnostics;
using System.Windows;
using REFame.PasswordManagement.App.CoreComponents;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.App.ViewModel;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.AppCore.Wpf;
using REFame.PasswordManagement.DependencyInjection;
using REFame.PasswordManagement.Update;

namespace REFame.PasswordManagement.App
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static string loginPw;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Debugger.IsAttached)
            {
                var updater = new AppUpdater();
                await updater.UpdateApp(); 
            }

            PWCore.Create();
            PWCore.CurrentCore
                .RegisterTypes()
                .CreateCoreBuilder()

                .UseCulture<CultureBuilder>()
                .UseLogin<LoginHandler>()
                .UseUI<UIBuilder>()

                .MainWindow<MainWindow>()
                .MainViewModel<MainViewModel>()

                .BuildCore()
                .RunWpf();
        }
    }
}