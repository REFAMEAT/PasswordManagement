using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.App.ViewModel;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Data;
using REFame.PasswordManagement.Database;
using REFame.PasswordManagement.Database.DbSet;
using REFame.PasswordManagement.Database.Model;
using REFame.PasswordManagement.File.Contracts.Binary;
using REFame.PasswordManagement.File.Contracts.Config;
using REFame.PasswordManagement.File.Module;
using REFame.PasswordManagement.Localization;
using REFame.PasswordManagement.Login;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Interfaces;
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
            MainWindow = new MainWindow();

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

            CreateFirstUserIfNecessary(PWCore.CurrentCore);

            InitCulture();

            var success = WpfCore.Current.Login(PWCore.CurrentCore);

            if (!success)
            {
                Shutdown(0);
            }
            else
            {
                MainWindow.DataContext = PWCore.CurrentCore.GetRegisteredType<MainViewModel>();
                MainWindow.Show();
            }
        }

        private void CreateFirstUserIfNecessary(ICore appCore)
        {
            var login = appCore.GetRegisteredType<ILogin>();
            var useDatabase = appCore
                .GetRegisteredType<IConfigurationFactory<DatabaseData>>()
                .SetPath()
                .Create()
                .Load()
                .UseDatabase;


            if (!login.NeedFirstUser())
            {
                return;
            }

            USERDATA firstUser = AddUser.CreateUser();

            if (firstUser != null && useDatabase)
            {
                var data = PWCore
                    .CurrentCore
                    .GetRegisteredType<IDataSet<USERDATA>>();
                data.Entities.Add(firstUser);
                data.SaveChanges();
            }
            else if (firstUser != null)
            {
                PWCore.CurrentCore
                    .GetRegisteredType<IBinaryHelperFactory>()
                    .SetPath()
                    .Create()
                    .Write(
                        new BinaryData(
                            firstUser.USUSERNAME,
                            firstUser.USPASSWORD,
                            firstUser.USSALT));
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

            CultureInfo cultureInfo = new CultureInfo(language ?? "en-001");

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            Loc.Culture = cultureInfo;
        }
    }
}