using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.App.ViewModel;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Configuration.Contracts;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.DependencyInjection;
using REFame.PasswordManagement.Localization;
using REFame.PasswordManagement.Login.Contracts;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Update;
using REFame.PasswordManagement.WpfBase.Mediator;

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
            var updater = new AppUpdater();
            await updater.UpdateApp();

            PWCore.Create();
            await PWCore.CurrentCore.RegisterTypes();
            await InitCulture();

            await InitUI();
            await CreateFirstUserIfNecessary(PWCore.CurrentCore);

            var success = WpfCore.Current.Login(PWCore.CurrentCore);

            if (!success)
            {
                Shutdown(0);
            }
            else
            {
                MainWindow = new MainWindow();
                MainWindow.DataContext = PWCore.CurrentCore.GetRegisteredType<MainViewModel>();
                MainWindow.Show();
            }
        }

        private async Task InitUI()
        {
            // Register method when theme-change is requested
            ThemeMediator.ChangeThemeRequested +=
                async (sender, args) => await UiHelper.AdjustApplicationStyle();

            // Adjust current App-Style
            await UiHelper.AdjustApplicationStyle();
        }

        private async Task CreateFirstUserIfNecessary(ICore appCore)
        {
            var login = appCore.GetRegisteredType<ILogin>();

            if (!login.NeedFirstUser())
            {
                return;
            }

            USERDATA firstUser = AddUser.CreateUser();

            IPwmDbContext data = PWCore
                .CurrentCore
                .GetRegisteredType<IPwmDbContextFactory>()
                .Create();
            data.USERDATA.Add(firstUser);
            await data.SaveChangesAsync();

        }

        private async Task InitCulture()
        {
            string language = (await PWCore.CurrentCore
                    .GetRegisteredType<IConfigurationFactory<ThemeData>>()
                    .SetPath()
                    .Create()
                    .LoadAsync())
                .Language;

            CultureInfo cultureInfo = new CultureInfo(language ?? "en-001");

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            Loc.Culture = cultureInfo;
        }
    }
}