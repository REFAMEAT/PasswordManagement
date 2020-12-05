using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.Settings.Contracts;
using REFame.PasswordManagement.Settings.ViewModel.Tabs;
using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.SettingFactories
{
    public class ThemeSettingsFactory : ISettingFactory
    {
        private ThemeSettingsViewModel viewModel;
        private SettingMediator mediator;
        public ISettingService<ITheme> OverrideSettingService { get; set; }

        public BindableBase GetViewModel()
        {
            return viewModel ??= new ThemeSettingsViewModel(OverrideSettingService);
        }

        public SettingMediator GetMediator()
        {
            if (mediator == null)
            {
                mediator = new SettingMediator();
                viewModel.SettingMediator = mediator;
            }

            return mediator;
        }

        public string GetHeader()
        {
            return "Theme";
        }
    }
}