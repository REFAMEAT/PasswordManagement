using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.ViewModel
{
    public class SettingsViewModel : BindableBase
    {
        public List<SettingMediator> SettingMediators { get; private set; }

        [CanBeNull]
        public Action OnCloseAction { get; set; }

        public SettingsViewModel()
        {
            SettingMediators = new List<SettingMediator>();
            CloseCommand = new AsyncCommand(async () => OnCloseAction?.Invoke());
            SaveCommand = new AsyncCommand(Save);
        }

        public ICommand CloseCommand { get; set; }
        public ICommand SaveCommand { get; }

        private Task Save()
        {
            SettingMediators.ForEach(x => x.RequestSave(this));
            OnCloseAction?.Invoke();
            return Task.CompletedTask;
        }
    }
}