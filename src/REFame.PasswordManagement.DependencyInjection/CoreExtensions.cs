using System.Threading.Tasks;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Configuration;
using REFame.PasswordManagement.Configuration.Contracts;
using REFame.PasswordManagement.Data;
using REFame.PasswordManagement.Data.Contracts;
using REFame.PasswordManagement.DB;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.File;
using REFame.PasswordManagement.Login;
using REFame.PasswordManagement.Login.Contracts;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.ProgressBar.Contracts;
using REFame.PasswordManagement.Services.Implementations;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.Settings;
using REFame.PasswordManagement.Settings.Contracts;
using REFame.PasswordManagement.Settings.SettingFactories;
using REFame.PasswordManagement.Settings.SettingFactories.Contracts;
using REFame.PasswordManagement.UserManagement;
using REFame.PasswordManagement.UserManagement.Contracts;

namespace REFame.PasswordManagement.DependencyInjection
{
    public static class CoreExtensions
    {
        public static async Task<ICore> RegisterTypes(this ICore appCore)
        {
            // Register types for Service
            appCore.RegisterType<ISettingService<DatabaseData>, DatabaseSettingService>();
            appCore.RegisterType<ISettingService<ThemeData>, ThemeSettingService>();

            // Register types for File
            appCore.RegisterType<IFolderProvider, FolderProvider>();
            appCore.RegisterType<IConfigurationFactory<DatabaseData>, ConfigurationFactory<DatabaseData>>();
            appCore.RegisterType<IConfigurationFactory<ThemeData>, ConfigurationFactory<ThemeData>>();

            // Register types for Settings
            appCore.RegisterType<IDatabaseSettingsFactory, DatabaseSettingsFactory>();
            appCore.RegisterType<IThemeSettingsFactory, ThemeSettingsFactory>();
            appCore.RegisterType<ISetting, Setting>();

            // Register types for Progress-Bar
            appCore.RegisterType<IProgressBar, ProgressBar.Contracts.ProgressBar>();

            // Register types for Database
            appCore.RegisterType<ISqlConnectionStringBuilder, SqlConnectionStringBuilder>();
            appCore.RegisterType<ISqLiteConnectionStringBuilder, SqLiteConnectionStringBuilder>();
            appCore.RegisterType<IPwmDbContextFactory, PwmDbDbContextFactory>();

            // Register DataManager and login
            appCore.RegisterType<IDataManager<PasswordData>, DatabaseDataManager>();
            appCore.RegisterType<ILogin, DatabaseLogin>();

            // Register types for UserManagement
            appCore.RegisterType<IUserMgmt, UserMgmt>();

            return appCore;
        }
    }
}