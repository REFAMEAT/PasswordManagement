using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.Settings.SettingFactories.Contracts;
using REFame.PasswordManagement.Settings.ViewModel.Tabs;
using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.SettingFactories
{
    public class DatabaseSettingsFactory : IDatabaseSettingsFactory
    {
        private DatabaseSettingsViewModel viewModel;
        private SettingMediator mediator;
        private ISettingService<DatabaseData> setting;

        public DatabaseSettingsFactory(ISettingService<DatabaseData> setting)
        {
            this.setting = setting;
        }

        //public ISettingService<DatabaseData> OverrideSettingService { get; set; }

        public BindableBase GetViewModel()
        {
            return viewModel ??= new DatabaseSettingsViewModel(setting);
        }

        public SettingMediator GetMediator()
        {
            if (mediator == null)
            {
                viewModel.SettingMediator = new SettingMediator();
                mediator = viewModel.SettingMediator;
            }

            return mediator;

        }

        public string GetHeader()
        {
            return "Database";
        }
    }
}