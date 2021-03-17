using System;
using System.Collections.Generic;
using REFame.PasswordManagement.Settings.ViewModel;
using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Mediator;

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

        public void SetOnClose(Action onCloseAction)
        {
            viewModel.OnCloseAction = onCloseAction;
        }
    }
}