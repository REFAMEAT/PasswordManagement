using System.Collections.Generic;
using REFame.PasswordManagement.Settings.ViewModel;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.Settings
{
    public class SettingsViewModelFactory : IBindableFactory
    {
        SettingsViewModel viewModel = new SettingsViewModel();
        
        public BindableBase GetViewModel()
        {
            return viewModel;
        }

        public void ReportMediators(List<SettingMediator> mediators)
        {
            viewModel.SettingMediators.AddRange(mediators);
        }
    }
}