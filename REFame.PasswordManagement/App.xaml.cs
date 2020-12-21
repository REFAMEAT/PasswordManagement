using System.Threading.Tasks;
using System.Windows;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Data;
using REFame.PasswordManagement.Database;
using REFame.PasswordManagement.File.Module;
using REFame.PasswordManagement.Logging;
using REFame.PasswordManagement.Login;
using REFame.PasswordManagement.ProgressBar;
using REFame.PasswordManagement.Services;
using REFame.PasswordManagement.Update;
using REFame.PasswordManagement.WpfBase.Localization;

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
            Current.DispatcherUnhandledException += (o, args) => Logger.Current.Get().Error(args.Exception);

            PWCore.Create();
            PWCore.CurrentCore
                .RegisterModule<ServiceModule>()
                .RegisterModule<FileModule>()
                .RegisterModule<LocalizationModule>()
                .RegisterModule<UISetup>()
                .RegisterModule<ProgressBarModule>()
                .RegisterModule<UpdateModule>()
                .RegisterModule<DatbaseModule>()
                .RegisterModule<LoginModule>()
                .RegisterModule<DataModule>();

            Task runTask = PWCore.CurrentCore.Run();
            await runTask;
            Task.WaitAll(runTask);
            bool success = WpfCore.Current.Login(PWCore.CurrentCore);

            if (success)
            {
                WpfCore.Current.RegisterMainWindow<MainWindow>();
            }
        }
    }
}