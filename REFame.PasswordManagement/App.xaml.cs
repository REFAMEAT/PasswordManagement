using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Data;
using REFame.PasswordManagement.Database;
using REFame.PasswordManagement.File.Contracts.Config;
using REFame.PasswordManagement.File.Module;
using REFame.PasswordManagement.Localization;
using REFame.PasswordManagement.Logging;
using REFame.PasswordManagement.Login;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.ProgressBar;
using REFame.PasswordManagement.Services;
using REFame.PasswordManagement.Settings;
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
            Current.DispatcherUnhandledException += (o, args) => Logger.Current.Get().Error(args.Exception);

            PWCore.Create();
            PWCore.CurrentCore
                .RegisterModule<ServiceModule>()
                .RegisterModule<FileModule>()
                .RegisterModule<SettingsModule>()
                .RegisterModule<UISetup>()
                .RegisterModule<ProgressBarModule>()
                .RegisterModule<UpdateModule>()
                .RegisterModule<DatbaseModule>()
                .RegisterModule<LoginModule>()
                .RegisterModule<DataModule>();

            Task runTask = PWCore.CurrentCore.Run();
            await runTask;
            Task.WaitAll(runTask);

            InitCulture();
            bool success = WpfCore.Current.Login(PWCore.CurrentCore);

            if (success)
            {
                WpfCore.Current.RegisterMainWindow<MainWindow>();
            }
            else
            {
                Shutdown(0);
            }
        }

        private void InitCulture()
        {
            string language = PWCore.CurrentCore
                .GetRegisteredType<IConfigurationFactory<ThemeData>>()
                .SetPath()
                .Create()
                .Load()
                .Language;

            CultureInfo cultureInfo = new CultureInfo(language);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            Loc.Culture = cultureInfo;
        }
    }
}