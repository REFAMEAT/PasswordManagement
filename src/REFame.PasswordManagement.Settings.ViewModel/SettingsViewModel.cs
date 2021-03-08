using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.ViewModel
{
    public class SettingsViewModel : BindableBase
    {
        public List<SettingMediator> SettingMediators { get; private set; }

        public SettingsViewModel()
        {
            SettingMediators = new List<SettingMediator>();
            SaveCommand = new AsyncCommand(Save);
        }

        public ICommand SaveCommand { get; }

        private Task Save()
        {
            SettingMediators.ForEach(x => x.RequestSave(this));
            return Task.CompletedTask;
        }
    }
}