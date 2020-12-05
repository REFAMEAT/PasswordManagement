using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.Settings.Contracts;
using REFame.PasswordManagement.Settings.ViewModel.Tabs;
using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.SettingFactories
{
    public class DatabaseSettingsFactory : ISettingFactory
    {
        private DatabaseSettingsViewModel viewModel;
        private SettingMediator mediator;

        public ISettingService<DatabaseData> OverrideSettingService { get; set; }

        public BindableBase GetViewModel()
        {
            return viewModel ??= new DatabaseSettingsViewModel(OverrideSettingService);
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